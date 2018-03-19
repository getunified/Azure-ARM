package nz.co.getunified.getunified.Device;

import android.animation.ArgbEvaluator;
import android.animation.ValueAnimator;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.CompoundButton;
import android.widget.Switch;
import android.widget.TextView;

import nz.co.getunified.getunified.R;

public class DeviceDetailActivity extends AppCompatActivity implements View.OnClickListener, CompoundButton.OnCheckedChangeListener {
    private WebView statWeb;
    private WaveView waveAbove, waveBelow;
    private TextView mainValue, status;
    private Handler handler;
    private Runnable task;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_device_detail);

        initComponents();

        //This part is the payment delay
        handler = new Handler();
        task = new Runnable() {
            public void run() {
                if (handler != null) {
                    DeviceValueAsync deviceValueAsync = new DeviceValueAsync(new SetValueCallBack() {
                        @Override
                        public void setValue(double value) {
                            mainValue.setText(value + "°C");
                            setColor(value);
                        }
                    }, 1);
                    deviceValueAsync.execute();

                    handler.postDelayed(this, 10 * 1000);
                }
            }
        };
        handler.post(task);
    }

    private void initComponents() {
        waveAbove = findViewById(R.id.wave_above);
        waveBelow = findViewById(R.id.wave_below);
        mainValue = findViewById(R.id.detail_device_value);
        status = findViewById(R.id.detail_status);

        Switch turnswitch = findViewById(R.id.switch_button);
        turnswitch.setOnCheckedChangeListener(this);

        statWeb = findViewById(R.id.statistics);
        WebSettings webSettings = statWeb.getSettings();
        webSettings.setJavaScriptEnabled(true);
        webSettings.setUseWideViewPort(true);
        webSettings.setLoadWithOverviewMode(true);
        statWeb.setWebViewClient(new WebViewClient());
        //statWeb.setInitialScale(160);
        statWeb.loadUrl("file:///android_asset/test.html");
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.back:
                finish();
                break;
            case R.id.menu:
                Intent intent = new Intent(this, DeviceSettingActivity.class);
                startActivity(intent);
        }
    }

    //The wave color is depending on the input value
    private void setColor(Double value) {
        int currentColor = waveAbove.getPaint().getColor();
        if (value < 40) {
            status.setTextColor(Color.parseColor("#FF888888"));
            ValueAnimator vColor = ValueAnimator.ofObject(new ArgbEvaluator(),
                    currentColor, Color.parseColor("#0080ff"));
            vColor.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
                @Override
                public void onAnimationUpdate(ValueAnimator valueAnimator) {
                    int color = (int) valueAnimator.getAnimatedValue();
                    Log.e("color", color + "");
                    waveAbove.getPaint().setColor(color);
                    waveAbove.getPaint().setAlpha(40);
                    waveBelow.getPaint().setColor(color);
                    waveBelow.getPaint().setAlpha(60);
                }
            });
            vColor.setDuration(1000);
            vColor.start();

        }
        if (value >= 40 && value < 65) {
            status.setTextColor(Color.YELLOW);
            ValueAnimator vColor = ValueAnimator.ofObject(new ArgbEvaluator(),
                    currentColor, Color.parseColor("#FFBB00"));
            vColor.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
                @Override
                public void onAnimationUpdate(ValueAnimator valueAnimator) {
                    int color = (int) valueAnimator.getAnimatedValue();
                    Log.e("color", color + "");
                    waveAbove.getPaint().setColor(color);
                    waveAbove.getPaint().setAlpha(40);
                    waveBelow.getPaint().setColor(color);
                    waveBelow.getPaint().setAlpha(60);
                }
            });
            vColor.setDuration(1000);
            vColor.start();

        }
        if (value >= 65) {
            status.setTextColor(Color.RED);
            ValueAnimator vColor = ValueAnimator.ofObject(new ArgbEvaluator(),
                    currentColor, Color.parseColor("#0080ff"));
            vColor.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
                @Override
                public void onAnimationUpdate(ValueAnimator valueAnimator) {
                    int color = (int) valueAnimator.getAnimatedValue();
                    Log.e("color", color + "");
                    waveAbove.getPaint().setColor(color);
                    waveAbove.getPaint().setAlpha(40);
                    waveBelow.getPaint().setColor(color);
                    waveBelow.getPaint().setAlpha(60);
                }
            });
            vColor.setDuration(1000);
            vColor.start();
        }
    }


    @Override
    public void onCheckedChanged(CompoundButton compoundButton, boolean b) {

    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
//        statWeb = null;
//        waveAbove = null;
//        waveBelow = null;
//        mainValue = null;
//        status = null;
        task = null;
        handler.post(null);
        handler = null;
    }

}
