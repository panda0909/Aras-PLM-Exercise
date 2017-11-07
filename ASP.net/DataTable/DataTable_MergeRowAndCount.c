private static  DataSet DataSetRemoveRepeat_MergeCount(DataSet ds_source)
{
	DataRow[] foundRows;
	DataSet ds_result = new DataSet();
	try
	{
		ds_result = ds_source.Clone();
		DataTable result = ds_result.Tables[0];
		foreach (DataRow dr in ds_source.Tables[0].Rows)
		{
			string son_item_number = dr["son"].ToString();
			int son_quantity =int.Parse( dr["quantity"].ToString());
			foundRows= result.Select("son = '"+ son_item_number + "'");
			if (foundRows.Count() > 0) {
				int index = result.Rows.IndexOf(foundRows[0]);
				int old_quantity=int.Parse( result.Rows[index]["quantity"].ToString());
				int new_quantity = old_quantity + son_quantity;
				result.Rows[index]["quantity"] = new_quantity;

			}
			else
			{
				DataRow newRow = dr;
				result.ImportRow(dr);
			}
		}
		return ds_result;
	}catch(Exception ex)
	{
		return null;
	}
}