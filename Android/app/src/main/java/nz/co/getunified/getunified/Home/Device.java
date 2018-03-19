package nz.co.getunified.getunified.Home;

/**
 * Created by FD-GHOST on 2018/2/14.
 */

public class Device {
    private int imageId;
    private String name, value;

    public Device(int imageId, String name, String value) {
        this.imageId = imageId;
        this.name = name;
        this.value = value;
    }

    public int getImageId() {
        return imageId;
    }

    public String getName() {
        return name;
    }

    public String getValue() {
        return value;
    }
}
