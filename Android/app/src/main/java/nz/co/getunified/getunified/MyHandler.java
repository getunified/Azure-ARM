package nz.co.getunified.getunified;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.microsoft.windowsazure.notifications.NotificationsHandler;

/**
 * Created by FD-GHOST on 2018/2/14.
 */

public class MyHandler extends NotificationsHandler {
    public static final int NOTIFICATION_ID = 1; //Same NOTIFICATION_ID in the mNotificationManager.notify will overwrite the previous message
    private NotificationManager mNotificationManager;
    private Context context;

    @Override
    public void onReceive(Context context, Bundle bundle) {
        this.context = context;
        String message = bundle.getString("message");

        //TODO Add Intent according to the incoming message to deal with payload
        Log.e("Bundle Message", bundle.toString());

        sendNotification(message);
    }

    private void sendNotification(String msg) {
        Intent intent = new Intent(context, MainActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

        mNotificationManager = (NotificationManager)
                context.getSystemService(Context.NOTIFICATION_SERVICE);

        PendingIntent contentIntent = PendingIntent.getActivity(context, 0,
                intent, PendingIntent.FLAG_ONE_SHOT);

        Uri defaultSoundUri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
        NotificationCompat.Builder mBuilder =
                new NotificationCompat.Builder(context)
                        .setSmallIcon(R.mipmap.ic_launcher)
                        .setContentTitle("GetUnified Notification")
                        .setStyle(new NotificationCompat.BigTextStyle()
                                .bigText(msg))
                        .setSound(defaultSoundUri)
                        .setContentText(msg);

        mBuilder.setContentIntent(contentIntent);
        mNotificationManager.notify(msg.hashCode(), mBuilder.build());
    }
}
