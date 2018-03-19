package nz.co.getunified.getunified.Online;

import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.GridView;
import android.widget.TextView;

import java.util.HashMap;

import nz.co.getunified.getunified.R;

public class OnlineFragment extends Fragment {
    private GridView gridView;
    private TextView sum, numItes;
    private HashMap<String, Cartitem> cartitemList;
    private Button checkout;

    public OnlineFragment() {
        // Required empty public constructor
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_online, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        gridView = getView().findViewById(R.id.online_gridview);
        sum = getView().findViewById(R.id.shopping_sum);
        numItes = getView().findViewById(R.id.shopping_count);
        cartitemList = new HashMap<String, Cartitem>();

        gridView.setAdapter(new OnlineAdapter(getActivity().getApplicationContext(), cartitemList, sum, numItes));

        checkout = getView().findViewById(R.id.shopping_checkout);
        checkout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getActivity().getApplicationContext(), CheckoutActivity.class);
                intent.putExtra("CartItemList", cartitemList);
                getActivity().getApplicationContext().startActivity(intent);
            }
        });
    }
}
