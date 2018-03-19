package nz.co.getunified.getunified;

import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MenuItem;

import com.google.android.gms.common.GoogleApiAvailability;
import com.microsoft.windowsazure.notifications.NotificationsManager;

import nz.co.getunified.getunified.Home.HomeFragment;
import nz.co.getunified.getunified.Online.OnlineFragment;

import static com.google.android.gms.common.api.CommonStatusCodes.SUCCESS;


public class MainActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {
    private HomeFragment homeFragment;
    private OnlineFragment onlineFragment;
    private FragmentManager manager;
    private FragmentTransaction transaction;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Log.e("App: ", "Running");

        isGooglePlayServicesAvailable(); // Check the google play before register to notification hub

        NotificationsManager.handleNotifications(this, NotificationSettings.SenderId, MyHandler.class);
        registerWithNotificationHubs();

        initComponents();
    }

    private void initComponents() {
        manager = getFragmentManager();
        transaction = manager.beginTransaction();
        homeFragment = new HomeFragment();
        onlineFragment = new OnlineFragment();
        transaction.add(R.id.container, homeFragment);
        transaction.commit();
        NavigationView navigationView = findViewById(R.id.nav);
        navigationView.setNavigationItemSelectedListener(this);
    }


    @Override
    protected void onResume() {
        super.onResume();
        isGooglePlayServicesAvailable();
    }

    private void isGooglePlayServicesAvailable() {
        int state = GoogleApiAvailability.getInstance().isGooglePlayServicesAvailable(this);
        if (state != SUCCESS)
            GoogleApiAvailability.getInstance().getErrorDialog(this, state, 0).show();
    }


    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
        DrawerLayout drawer = findViewById(R.id.drawerlayout);
        drawer.closeDrawer(GravityCompat.START);
        switch (item.getItemId()) {
            case R.id.nav_devices:
                transaction = manager.beginTransaction();
                transaction.replace(R.id.container, homeFragment);
                transaction.commit();
                break;
            case R.id.nav_online:
                transaction = manager.beginTransaction();
                transaction.replace(R.id.container, onlineFragment);
                transaction.commit();
                break;
            //TODO Add the logout
        }
        return true;
    }


    private void registerWithNotificationHubs() {
        // Start IntentService to register this application with FCM.
        Intent intent = new Intent(this, RegistrationIntentService.class);
        startService(intent);
    }
}
