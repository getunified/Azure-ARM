package nz.co.getunified.getunified.Online;

import java.io.Serializable;

/**
 * Created by FD-GHOST on 2018/2/28.
 */

public class Cartitem implements Serializable {
    private Product product;
    private int number;

    public Cartitem(Product product, int number) {
        this.product = product;
        this.number = number;
    }

    public Product getProduct() {
        return product;
    }

    public int getNumber() {
        return number;
    }
}
