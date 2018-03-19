package nz.co.getunified.getunified.Home;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;

import nz.co.getunified.getunified.R;

public class HomeFragment extends Fragment implements View.OnTouchListener {
    private String mParam1;

    private ListView listView;
    private TextView textView;
    double init, dist, current;

    public HomeFragment() {
        // Required empty public constructor
    }

    public static HomeFragment newInstance(String param1) {
        HomeFragment fragment = new HomeFragment();
//        Bundle args = new Bundle();
//        args.putString("param1", param1);
//        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        if (getArguments() != null) {
//            mParam1 = getArguments().getString("param1");
//        }

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_home, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        listView = getView().findViewById(R.id.list);
        listView.setAdapter(new HomeAdapter(getActivity().getApplicationContext()));

        listView.setOnTouchListener(this);
        textView = getView().findViewById(R.id.tv);
        current = 0;
    }

    public static void setMargins(View v, int l, int t, int r, int b) {
        if (v.getLayoutParams() instanceof ViewGroup.MarginLayoutParams) {
            ViewGroup.MarginLayoutParams p = (ViewGroup.MarginLayoutParams) v.getLayoutParams();
            p.setMargins(l, t, r, b);
            v.requestLayout();
        }
    }

    //This is use to control the map hidden below the listview, to detect the movement
    @Override
    public boolean onTouch(View view, MotionEvent motionEvent) {
        //The value here is refer to the value defined in the xml
        int midposition = dp2px(180);
        int botposition = dp2px(360);

        switch (motionEvent.getAction()) {
            case MotionEvent.ACTION_DOWN://0
                init = motionEvent.getY();
                break;
            case MotionEvent.ACTION_UP://1
                if (current > midposition) {
                    for (double i = current; i < botposition; i++) {
                        current = i;
                        setMargins(listView, 0, (int) current, 0, 0);
                        textView.setAlpha((float) (current / botposition));
                    }
                } else {
                    for (double i = current; i > 0; i--) {
                        current = i;
                        setMargins(listView, 0, (int) current, 0, 0);
                        textView.setAlpha((float) (current / botposition));
                    }
                }
                break;
            case MotionEvent.ACTION_MOVE://2
                dist = init - motionEvent.getY();
                if (current <= botposition && 0 <= current) {
                    current = current - dist;
                    current = (current > botposition) ? botposition : current;
                    current = (current < 0) ? 0 : current;
                    setMargins(listView, 0, (int) current, 0, 0);
                    textView.setAlpha((float) (current / botposition));
                }
                break;
        }
        return false;
    }

    private int dp2px(float dpValue) {
        final float scale = getResources().getDisplayMetrics().density;
        return (int) (dpValue * scale + 0.5f);
    }
}
