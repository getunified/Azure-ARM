package nz.co.getunified.getunified.Device;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;

import nz.co.getunified.getunified.R;

public class DeviceSettingActivity extends AppCompatActivity implements View.OnClickListener {
    private EditText lowerLimit, upperLimit;




    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_device_setting);
        lowerLimit = findViewById(R.id.device_setting_lowerlimit_value);
        upperLimit = findViewById(R.id.device_setting_upperlimit_value);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.back:
                finish();
                break;
            case R.id.device_setting_name_save:
                double lowerLimit = Double.parseDouble(this.lowerLimit.getText().toString());
                double upperlimit = Double.parseDouble(this.upperLimit.getText().toString());
                new SetDeviceAsync(this, 1, lowerLimit, upperlimit).execute();
                break;
        }
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }
}
