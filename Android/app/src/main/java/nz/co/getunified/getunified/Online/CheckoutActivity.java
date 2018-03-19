package nz.co.getunified.getunified.Online;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.HashMap;

import nz.co.getunified.getunified.R;

import static android.view.View.GONE;
import static android.view.View.VISIBLE;

public class CheckoutActivity extends AppCompatActivity {
    private ListView listView;
    private TextView sum, numItes;
    private HashMap<String, Cartitem> cartitemList;
    private Button pay;
    private ProgressBar progress;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_checkout);
        listView = findViewById(R.id.checkout_list);
        sum = findViewById(R.id.checkout_sum);
        numItes = findViewById(R.id.checkout_count);
        cartitemList = (HashMap<String, Cartitem>) getIntent().getSerializableExtra("CartItemList");
        listView.setAdapter(new CheckoutAdapter(getApplicationContext(), cartitemList, sum, numItes));
        pay = findViewById(R.id.checkout_pay);
        progress = findViewById(R.id.checkout_progess);
        progress.setVisibility(GONE);
        pay.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                progress.setVisibility(VISIBLE);
                new Handler().postDelayed(new Runnable() {
                    public void run() {
                        progress.setVisibility(GONE);
                        Intent intent = new Intent(getApplicationContext(), PayResultActivity.class);
                        startActivity(intent);
                        finish();
                    }
                }, 3000);
            }
        });
    }
}
