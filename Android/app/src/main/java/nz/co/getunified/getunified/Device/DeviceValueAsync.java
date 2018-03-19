package nz.co.getunified.getunified.Device;

import android.os.AsyncTask;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

/**
 * Created by FD-GHOST on 2018/3/1.
 */

public class DeviceValueAsync extends AsyncTask<Void, Void, Double> {
    private Double value;
    private String result;
    private int sensorID;
    private SetValueCallBack setValueCallBack;

    public DeviceValueAsync(SetValueCallBack setValueCallBack, int sensorID) {
        this.setValueCallBack = setValueCallBack;
        this.sensorID = sensorID;
    }

    @Override
    protected Double doInBackground(Void... voids) {
        OkHttpClient client = new OkHttpClient();
        Request request = new Request.Builder()
                .url("http://getunified.azurewebsites.net/api/Sensor/GetSensorValue?sensorID=" + sensorID)
                .build();
        Response response = null;
        try {
            response = client.newCall(request).execute();
            result = response.body().string().replace("[", "");
            result = result.replace("]", "");
            JSONObject resultJson;
            Log.e("result is: ", result);
            try {
                resultJson = new JSONObject(result);
                value = resultJson.getDouble("value");
                if (value.isNaN()) {
                    return null;
                } else {
                    return value;
                }

            } catch (JSONException e) {
                Log.e("JSONException", e.getMessage());
                return null;
            }
        } catch (IOException e) {
            Log.e("JSONException", e.getMessage());
            return null;
        }
    }

    @Override
    protected void onPostExecute(Double s) {
        super.onPostExecute(s);
        setValueCallBack.setValue((double) Math.round(s * 100) / 100);
    }
}
