public class SortModel
{
    public enum SortType { Strength, Id, Atk, Def, Hp }
    public enum OrderType { Ascending, Descending }

    // Œ»İ‚Ì‘I‘ğó‘Ô‚ğ‚Ü‚Æ‚ß‚é\‘¢‘Ì
    public struct SortState
    {
        public SortType Type;
        public OrderType Order;

        public SortState(SortType arg_type, OrderType arg_order)
        {
            Type = arg_type;
            Order = arg_order;
        }

        // •ÏX‚ª‚ ‚Á‚½‚©‚Ì”äŠr
        public bool IsChanged(SortState arg_other)
        {
            return Type == arg_other.Type && Order == arg_other.Order;
        }
    }
}