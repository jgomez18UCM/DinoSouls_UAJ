using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telemetria;

public class ItemEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class GetItem : Telemetria.Event
    {
        string item_name { get; set; }
        float position_x { get; set; }
        float position_y { get; set; }
        public GetItem(string itemN, float x, float y) : base()
        {
            event_type = "GetItem";
            position_x = x;
            position_y = y;
            item_name = itemN;

        }

        public override string serialize()
        {
            return base.serialize() + $"{item_name},{position_x},{position_y}";
        }
    }

    public class UseItem : Telemetria.Event
    {
        string item_name { get; set; }
        public UseItem(string itemN, float x, float y) : base()
        {
            event_type = "UseItem";
            item_name = itemN;
        }

        public override string serialize()
        {
            return base.serialize() + $"{item_name}";
        }
    }
}
