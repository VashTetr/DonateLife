using System.Data;
using System.Reflection;

public static class DataRowMapper
{
    public static T ConvertDataRowToType<T>(this DataRow row, T obj)
    {
        // Get all the properties of the type T
        PropertyInfo[] properties = obj.GetType().GetProperties();

        // Iterate over each property
        foreach (var property in properties)
        {
            // Check if the DataRow contains a column with the same name as the property
            if (row.Table.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
            {
                // Set the property value if the column exists and is not null
                property.SetValue(obj, Convert.ChangeType(row[property.Name], property.PropertyType), null);
            }
        }

        return obj;
    }
}