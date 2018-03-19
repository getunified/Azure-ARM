package nz.co.getunified.getunified.Online;

import java.io.Serializable;

/**
 * Created by FD-GHOST on 2018/2/14.
 */

public class Product implements Serializable {
    private int imaeId;
    private double price;
    private String name;

    public Product(int imaeId, String name, double price) {
        this.imaeId = imaeId;
        this.name = name;
        this.price = price;
    }

    public int getImaeId() {
        return imaeId;
    }

    public String getName() {

        return name;
    }

    public double getPrice() {
        return price;
    }


}
