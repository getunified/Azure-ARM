package nz.co.getunified.getunified.Home;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

import nz.co.getunified.getunified.Device.DeviceDetailActivity;
import nz.co.getunified.getunified.R;

/**
 * Created by FD-GHOST on 2018/1/30.
 * Home Adapter, use the call the async to fetch the devices list
 */

public class HomeAdapter extends BaseAdapter implements View.OnClickListener {
    private Context context;
    private ArrayList<Device> list;
    private LayoutInflater inflater;

    public HomeAdapter(Context context) {
        this.context = context;
        list = new ArrayList<Device>();
        //TODO should to call the async here to fetch the data
        list.add(new Device(R.drawable.sensor, "Pump Temp", "25.1 °C"));
        list.add(new Device(R.drawable.sensor, "Water Tank Temp", "25.0 °C"));
        this.inflater = LayoutInflater.from(context);
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
        View view = inflater.inflate(R.layout.home_item, null);
        view.setOnClickListener(this);
        ImageView icon = view.findViewById(R.id.list_item_icon);
        icon.setImageDrawable(context.getResources().getDrawable(list.get(position).getImageId()));
        TextView name = view.findViewById(R.id.list_item_name);
        name.setText(list.get(position).getName());
        TextView value = view.findViewById(R.id.list_item_value);
        value.setText(list.get(position).getValue());
        return view;
    }

    @Override
    public void onClick(View view) {
        Intent intent = new Intent(context, DeviceDetailActivity.class);
        context.startActivity(intent);
    }
}
