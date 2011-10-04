namespace CosmicFlow.AIMA.Core.Util.DataStructure
{
    using System.Collections.Generic;
    using System.Text;
    using System;
    /**
     * @author Ravi Mohan
     * 
     */
    public class Table<RowHeaderType, ColumnHeaderType, Value_Type> where Value_Type : struct
    {
        private List<RowHeaderType> rowHeaders;
        private List<ColumnHeaderType> columnHeaders;
        private Dictionary<RowHeaderType, Dictionary<ColumnHeaderType, Value_Type>> rows;

        public Table(List<RowHeaderType> rowHeaders,
                List<ColumnHeaderType> columnHeaders)
        {

            this.rowHeaders = rowHeaders;
            this.columnHeaders = columnHeaders;
            this.rows = new Dictionary<RowHeaderType, Dictionary<ColumnHeaderType, Value_Type>>();
            foreach (RowHeaderType rowHeader in rowHeaders)
            {
                rows.Add(rowHeader, new Dictionary<ColumnHeaderType, Value_Type>());
            }
        }

        public void set(RowHeaderType r, ColumnHeaderType c, Value_Type v)
        {
            if (rows[r].ContainsKey(c))
            {
                rows[r][c] = v;
            }
            else
            {
                rows[r].Add(c, v);
            }
        }

        public Value_Type? get(RowHeaderType r, ColumnHeaderType c)
        {
            if (!rows.ContainsKey(r))
            {
                return null;
            }
            Dictionary<ColumnHeaderType, Value_Type> rowValues = rows[r];

            if (rowValues == null || !rowValues.ContainsKey(c))
            {
                return null;
            }
            return rowValues[c];
        }

        public override System.String ToString()
        {
            StringBuilder buf = new StringBuilder();
            foreach (RowHeaderType r in rowHeaders)
            {
                foreach (ColumnHeaderType c in columnHeaders)
                {
                    buf.Append(get(r, c).ToString());
                    buf.Append(" ");
                }
                buf.Append("\n");
            }
            return buf.ToString();
        }

        class Row<R>
        {
            private Dictionary<ColumnHeaderType, Value_Type> _cells;

            public Row()
            {

                this._cells = new Dictionary<ColumnHeaderType, Value_Type>();
            }

            public Dictionary<ColumnHeaderType, Value_Type> cells()
            {
                return this._cells;
            }

        }

        class Cell<ValueHeaderType>
        {
            private ValueHeaderType _value;

            public Cell()
            {
                _value = default(ValueHeaderType);
            }

            public Cell(ValueHeaderType value)
            {
                this._value = value;
            }

            public void set(ValueHeaderType value)
            {
                this._value = value;
            }

            public ValueHeaderType value()
            {
                return _value;
            }

        }
    }
}