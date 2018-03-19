package nz.co.getunified.getunified;

import android.app.Application;

/**
 * Created by FD-GHOST on 2018/2/21.
 */

public class GlobalApp extends Application {
    private User user;

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }
}
