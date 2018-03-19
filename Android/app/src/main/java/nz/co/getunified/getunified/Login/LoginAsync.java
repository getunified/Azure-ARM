package nz.co.getunified.getunified.Login;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

import nz.co.getunified.getunified.GlobalApp;
import nz.co.getunified.getunified.MainActivity;
import nz.co.getunified.getunified.User;
import okhttp3.FormBody;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

/**
 * Created by FD-GHOST on 2018/2/20.
 */

public class LoginAsync extends AsyncTask<Void, Void, Boolean> {
    private GlobalApp application;
    private Activity activity;
    private Context context;
    private String username, password, result;
    private JSONObject resultJson, userJson;

    public LoginAsync(Application application, Activity activity, Context context, String username, String password) {
        this.application = (GlobalApp) application;
        this.activity = activity;
        this.context = context;
        this.username = username;
        this.password = password;
    }

    @Override
    protected Boolean doInBackground(Void... voids) {

        OkHttpClient client = new OkHttpClient();
        Log.e("Username & Password is ", username + password);
        RequestBody formBody = new FormBody.Builder()
                .add("username", username)
                .add("password", password)
                .build();
        Request request = new Request.Builder()
                .url("http://getunified.azurewebsites.net/api/user/userLogin")
                .post(formBody)
                .build();
        Response response = null;
        try {
            response = client.newCall(request).execute();
            result = response.body().string().replace("\\", "");
            result = result.substring(1, result.length() - 1);

            Log.e("result is: ", result);
            try {
                resultJson = new JSONObject(result);
                Log.e("result is: ", resultJson.optString("Status").equals("1") + "");
                if (resultJson.optString("Status").equals("1")) {
                    Log.e("result is: ", resultJson.optString("User"));
                    userJson = resultJson.optJSONObject("User");
                    return true;
                } else {
                    return false;
                }
            } catch (JSONException e) {
                Log.e("JSONException", e.getMessage());
                return false;
            }
        } catch (IOException e) {
            Log.e("JSONException", e.getMessage());
            return false;
        }
    }

    @Override
    protected void onPostExecute(Boolean aBoolean) {
        super.onPostExecute(aBoolean);
        if (aBoolean) {
            try {
                application.setUser(new User(
                        userJson.getInt("Uid"),
                        userJson.getString("Username"),
                        userJson.getString("Password"),
                        userJson.getString("Email"),
                        userJson.getString("Phone")));
            } catch (JSONException e) {
                e.printStackTrace();
            }
            Log.e("Login user", application.getUser().toString());
            Intent intent = new Intent(context, MainActivity.class);
            context.startActivity(intent);
            activity.finish();
        } else {
            Toast.makeText(context, "Login Fail", Toast.LENGTH_LONG).show();
        }
    }
}
