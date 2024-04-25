using Telemetria;


public class GetItemEvent : Telemetria.Event
{
    string item_name { get; set; }
    float position_x { get; set; }
    float position_y { get; set; }
    public GetItemEvent(string itemN, float x, float y) : base()
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

public class UseItemEvent : Telemetria.Event
{
    string item_name { get; set; }
    public UseItemEvent(string itemN, float x, float y) : base()
    {
        event_type = "UseItem";
        item_name = itemN;
    }

    public override string serialize()
    {
        return base.serialize() + $"{item_name}";
    }
}

