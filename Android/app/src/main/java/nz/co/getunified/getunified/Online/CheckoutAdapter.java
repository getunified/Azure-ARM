package nz.co.getunified.getunified.Online;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import nz.co.getunified.getunified.R;

/**
 * Created by FD-GHOST on 2018/2/28.
 */

public class CheckoutAdapter extends BaseAdapter implements View.OnClickListener {
    private Context context;
    private LayoutInflater inflater;
    private HashMap<String, Cartitem> cartitemList;
    private TextView sum, count;
    private List<String> itemList;

    public CheckoutAdapter(Context context, HashMap<String, Cartitem> cartitemList, TextView sum, TextView count) {
        this.context = context;
        this.inflater = LayoutInflater.from(context);
        this.cartitemList = cartitemList;
        this.sum = sum;
        this.count = count;
        itemList = new ArrayList<String>();
        itemList.addAll(cartitemList.keySet());
    }

    @Override
    public int getCount() {
        return cartitemList.size();
    }

    @Override
    public Object getItem(int position) {
        return null;
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        inflater = LayoutInflater.from(context);
        View view = inflater.inflate(R.layout.checkout_item, null);
        view.setOnClickListener(this);
        final ImageView icon = view.findViewById(R.id.checkoutitem_icon);
        final int index = position;
        final TextView name = view.findViewById(R.id.checkoutitem_name);
        final TextView value = view.findViewById(R.id.checkoutitem_price);
        final EditText numberOfItem = view.findViewById(R.id.checkoutitem_count);
        ImageButton numReduce = view.findViewById(R.id.checkoutitem_reduce);
        ImageButton numPlue = view.findViewById(R.id.checkoutitem_plus);
        final TextView singleSum = view.findViewById(R.id.checkoutitem_sum);
        numberOfItem.setText(cartitemList.get(itemList.get(position)).getNumber() + "");


        singleSum.setText("$" + cartitemList.get(itemList.get(position)).getProduct().getPrice() * cartitemList.get(itemList.get(position)).getNumber());
        icon.setImageDrawable(context.getResources().getDrawable(cartitemList.get(itemList.get(position)).getProduct().getImaeId()));
        name.setText(cartitemList.get(itemList.get(position)).getProduct().getName());
        value.setText("$" + cartitemList.get(itemList.get(position)).getProduct().getPrice());

        numReduce.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int current = Integer.valueOf(numberOfItem.getText().toString());
                if (current > 0) {
                    current -= 1;
                    numberOfItem.setText(current + "");
                    cartitemList.put(name.getText().toString(), new Cartitem(cartitemList.get(itemList.get(index)).getProduct(), current));

                    singleSum.setText("$" + cartitemList.get(itemList.get(index)).getProduct().getPrice() * cartitemList.get(itemList.get(index)).getNumber());
                    RefreshCart();
                }
            }
        });
        numPlue.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int current = Integer.valueOf(numberOfItem.getText().toString());
                if (current < 99) {
                    current += 1;
                    numberOfItem.setText(current + "");
                    cartitemList.put(name.getText().toString(), new Cartitem(cartitemList.get(itemList.get(index)).getProduct(), current));
                    singleSum.setText("$" + cartitemList.get(itemList.get(index)).getProduct().getPrice() * cartitemList.get(itemList.get(index)).getNumber());
                    RefreshCart();
                }
            }
        });

        RefreshCart();
        return view;
    }

    @Override
    public void onClick(View view) {
//        Intent intent = new Intent(context, DeviceDetailActivity.class);
//        context.startActivity(intent);
        Toast.makeText(context, "More detail page", Toast.LENGTH_LONG).show();
    }

    private void RefreshCart() {
        double cartSum = 0;
        int cartCount = 0;
        for (String key : cartitemList.keySet()) {
            cartSum += cartitemList.get(key).getProduct().getPrice() * cartitemList.get(key).getNumber();
            cartCount += cartitemList.get(key).getNumber();
        }
        sum.setText("$" + cartSum);
        count.setText(cartCount + " item(s)");
    }
}

