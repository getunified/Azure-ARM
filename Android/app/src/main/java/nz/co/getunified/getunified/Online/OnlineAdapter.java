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

import nz.co.getunified.getunified.R;

/**
 * Created by FD-GHOST on 2018/2/14.
 */

public class OnlineAdapter extends BaseAdapter implements View.OnClickListener {
    private Context context;
    private ArrayList<Product> list;
    private LayoutInflater inflater;
    private HashMap<String, Cartitem> cartitemList;
    private TextView sum, count;

    public OnlineAdapter(Context context, HashMap<String, Cartitem> cartitemList, TextView sum, TextView count) {
        this.context = context;
        list = new ArrayList<Product>();
        list.add(new Product(R.drawable.ur, "Ultrasound Sensor", 66.00));
        list.add(new Product(R.drawable.sensor, "Temperature Sensor", 33.00));
        list.add(new Product(R.drawable.weight, "Weight Sensor", 11.00));
        list.add(new Product(R.drawable.lpg, "LPG Bottle", 36.00));
        this.inflater = LayoutInflater.from(context);
        this.cartitemList = cartitemList;
        this.sum = sum;
        this.count = count;
    }

    @Override
    public int getCount() {
        return list.size();
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
        View view = inflater.inflate(R.layout.online_item, null);
        view.setOnClickListener(this);
        ImageView icon = view.findViewById(R.id.onlineitem_image);
        final int index = position;
        final TextView name = view.findViewById(R.id.onlineitem_name);
        final TextView value = view.findViewById(R.id.onlineitem_price);
        final EditText numberOfItem = view.findViewById(R.id.onlineitem_num);
        ImageButton numReduce = view.findViewById(R.id.onlineitem_num_reduce);
        ImageButton numPlue = view.findViewById(R.id.onlineitem_num_plus);
//        ImageButton addToCart = view.findViewById(R.id.onlineitem_add);

        icon.setImageDrawable(context.getResources().getDrawable(list.get(position).getImaeId()));
        name.setText(list.get(position).getName());
        value.setText("$" + list.get(position).getPrice());
        numReduce.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int current = Integer.valueOf(numberOfItem.getText().toString());
                if (current > 0) {
                    current -= 1;
                    numberOfItem.setText(current + "");
                    cartitemList.put(name.getText().toString(), new Cartitem(list.get(index), current));
                    RefreshCart();
                }
                if (current == 0) {
                    if (cartitemList.containsKey(name.getText().toString())) {
                        cartitemList.remove(name.getText().toString());
                        RefreshCart();
                    }
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
                    cartitemList.put(name.getText().toString(), new Cartitem(list.get(index), current));
                    RefreshCart();
                }
            }
        });


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
