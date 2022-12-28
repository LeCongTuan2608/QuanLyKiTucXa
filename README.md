# c-sharp
 // ================================ connection ==================================================
        SqlConnection conn;
        public static string connectString = @"DataSource = sdfsdf; Initial Dialog = Demo; Integrated Security = True;";
        public bool Open(){
                try{
                        conn = new SqlConnection(connectString);
                        conn.Open();
                        return true
                }catch(Exception ex){
                      MessageBox.Show("connection Error"+ ex.Message);  
                }
                return false
        
        }
        
        public void Close(){
                conn.Close();
                conn.Dispose();    
        }
        
        public SqlDataReader ExecuteReader(string sql){
                try{
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        return reader;
                }catch(Exception ex){
                        MessageBox.Show(ex.Message);
                }
                return null;
        
        
        }
 // ==================================================================================


        // ================================ EXCEL ==================================================
        #region //Các hàm xuất excel
        public static System.Data.DataSet getDataSet(DataTable dt)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            foreach (DataTable dataTable in getInfo(dt, "Names"))
            {
                ds.Tables.Add(dataTable);
            }
            return ds;
        }
        private static List<DataTable> getInfo(DataTable tablesplit, string tblName)
        {
            List<DataTable> tables = new List<DataTable>();
            DataTable dt1 = new DataTable();
            dt1 = tablesplit;
            dt1.TableName = tblName;
            int cnt = dt1.Rows.Count;
            tables = SplitTable(dt1, 5000, tblName);
            return tables;
        }
        private static List<DataTable> SplitTable(DataTable originalTable, int batchSize, string tblName)
        {
            List<DataTable> tables = new List<DataTable>();
            DataTable new_table = new DataTable();
            new_table = originalTable.Clone();
            int j = 0;
            int k = 1;
            // add 'count' to record names of sheets.
            int count = 1;
            if (originalTable.Rows.Count < batchSize)
            {
                new_table.TableName = tblName + "-" + count;
                new_table = originalTable.Copy();
                tables.Add(new_table.Copy());
            }
            else
            {
                for (int i = 0; i < originalTable.Rows.Count; i++)
                {
                    new_table.NewRow();
                    new_table.ImportRow(originalTable.Rows[i]);
                    if (++j == batchSize)
                    {
                        new_table.TableName = tblName + "_" + count;
                        tables.Add(new_table.Copy());
                        new_table.Rows.Clear();
                        count++;
                        k++;
                        j = 0;
                    }
                    if ((i + 1) == originalTable.Rows.Count)
                    {
                        new_table.TableName = tblName + "_" + count;
                        tables.Add(new_table.Copy());
                        count = 0;
                        k++;
                    }
                }
            }
            return tables;
        }
        #endregion
        private void button13_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView2.DataSource;
            System.Data.DataSet ds = getDataSet(dt);
            for (int count = 0; count < ds.Tables.Count; count++)
            {
                ds.Tables[count].TableName = "Sheet_" + count;
            }
            XLWorkbook wb = new XLWorkbook();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                wb.Worksheets.Add(ds.Tables[i], ds.Tables[i].TableName);
            }
            string tenfile = comboBox2.SelectedItem.ToString() + "-" + DateTime.Now.ToString("dd_MM_yyyy_hhmmss") + ".xlsx";
            FileStream file = new FileStream(@"D:\CNTT-K61\Learn\LapTrinhTrucQuan\Nhom 8\BaiTapLon\NEW\QuanLyKiTucXa\Excel\" + tenfile + "", FileMode.Create);
            wb.SaveAs(file);
            file.Close();
            MessageBox.Show("Xuất file " + tenfile.ToString() + @" thành công .Tại ổ D thư mục QuanLyKiTucXa\Excel!");
            this.Cursor = Cursors.Default;
        }
        // ==================================================================================
