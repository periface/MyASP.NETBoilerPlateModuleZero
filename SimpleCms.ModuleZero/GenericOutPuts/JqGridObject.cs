using System.Collections.Generic;

namespace SimpleCms.ModuleZero.GenericOutPuts
{
    public class JqGridObject
    {
        protected JqGridObject()
        {

        }
        public double total { get; protected set; }
        public int page { get; protected set; }
        public int records { get; protected set; }
        public string SortColumn { get; protected set; }
        public string SortOrder { get; protected set; }
        public object Data { get; protected set; }

        public static JqGridObject CreateModel<T>(int page, int records, double total, string sortColumn, string sortOrder, List<T> data)
        {
            return new JqGridObject()
            {
                Data = data,
                page = page,
                total = total,
                records = records,
                SortColumn = sortColumn,
                SortOrder = sortOrder
            };
        }
    }
}
