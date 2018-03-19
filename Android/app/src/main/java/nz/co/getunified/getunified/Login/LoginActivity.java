package nz.co.getunified.getunified.Login;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;

import nz.co.getunified.getunified.R;

public class LoginActivity extends AppCompatActivity {
    private EditText usernameValue, passwordValue;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        initComponents();
    }

    private void initComponents() {
        usernameValue = findViewById(R.id.username_value);
        passwordValue = findViewById(R.id.password_value);
    }

    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.login:
                //TODO Need to add more restrictions before call the API
                new LoginAsync(getApplication(), this, this, usernameValue.getText().toString(), passwordValue.getText().toString()).execute();
                break;

            case R.id.register:
                break;

            case R.id.forgetpassword:
                break;
        }
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }
}
