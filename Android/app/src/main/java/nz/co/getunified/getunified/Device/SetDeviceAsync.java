package nz.co.getunified.getunified.Device;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

/**
 * Created by FD-GHOST on 2018/3/1.
 */

public class SetDeviceAsync extends AsyncTask<Void, Void, Boolean> {
    //TODO Should verify the username and password

    private Context context;
    private int sensorId;
    private Double upperLimit, lowerLimit;
    private String result;

    public SetDeviceAsync(Context context, int sensorId, Double lowerLimit, Double upperLimit) {
        this.context = context;
        this.sensorId = sensorId;
        this.upperLimit = upperLimit;
        this.lowerLimit = lowerLimit;
    }

    @Override
    protected Boolean doInBackground(Void... voids) {
        MediaType JSON = MediaType.parse("application/json; charset=utf-8");
        OkHttpClient client = new OkHttpClient();
//        RequestBody formBody = new FormBody.Builder()
//                .add("sensorID", sensorId+"")
//                .add("upperLimit", upperLimit+"")
//                .add("lowerLimie",lowerLimit+"")
//                .build();
//        Request request = new Request.Builder()
//                .url("http://getunified.azurewebsites.net/api/Sensor/SetSensorLimits")
//                .post(formBody)
//                .build();
        String json = "{ \"sensorID\":\"1\", \"lowerLimit\":\"" + lowerLimit + "\", \"upperLimit\":\"" + upperLimit + "\"}";
        RequestBody body = RequestBody.create(JSON, json);
        Request request = new Request.Builder()
                .url("http://getunified.azurewebsites.net/api/Sensor/SetSensorLimitsJSON")
                .post(body)
                .build();
        Response response = null;
        try {
            response = client.newCall(request).execute();
            result = response.body().string();
            JSONObject resultJson;
            Log.e("result is: ", result);
            try {
                resultJson = new JSONObject(result);
                if (resultJson.getBoolean("isSuccessStatusCode")) {
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
    protected void onPostExecute(Boolean result) {
        super.onPostExecute(result);
        if (result) {
            Toast.makeText(context, "Saved", Toast.LENGTH_LONG).show();
        } else {
            Toast.makeText(context, "Failed", Toast.LENGTH_LONG).show();
        }
    }
}
