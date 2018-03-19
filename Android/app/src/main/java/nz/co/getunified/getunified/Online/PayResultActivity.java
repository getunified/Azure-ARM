package nz.co.getunified.getunified.Online;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;

import nz.co.getunified.getunified.R;

public class PayResultActivity extends AppCompatActivity implements View.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pay_result);
    }

    @Override
    public void onClick(View view) {
        finish();
    }
}
