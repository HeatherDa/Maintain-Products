using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maintain_Products
{
    public partial class ProductMaintenance : Form
    {
        public ProductMaintenance()
        {
            InitializeComponent();
            productCodeToolStripTextBox.Focus();
        }

        private void productsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.productsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.techSupport_DataDataSet);
                
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("A concurrency error occured.  Some rows were not updated.", "Concurrency Exception");
                this.productsTableAdapter.Fill(techSupport_DataDataSet.Products);
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                productsBindingSource.CancelEdit();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
            }
            finally
            {
                txtProdCode.ReadOnly = true; //product code read only unless entering new product
                productCodeToolStripTextBox.Focus();//ready to search for next entry
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'techSupport_DataDataSet.Products' table. You can move, or remove it, as needed.
            try
            {
                this.productsTableAdapter.Fill(this.techSupport_DataDataSet.Products);
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Database error # "+ex.Number+": "+ex.Message, ex.GetType().ToString());
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            txtProdCode.ReadOnly = false; //ready to put in product code for new product
            txtProdCode.Focus(); //ready to enter first item for new product
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            txtProdCode.ReadOnly = true;  //make sure people can't start a new product, delete it, and thereby be able to edit productcode of other products
            productCodeToolStripTextBox.Focus();
        }

        private void fillByProductCodeToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                string productCode = productCodeToolStripTextBox.Text;
                this.productsTableAdapter.FillByProductCode(this.techSupport_DataDataSet.Products, productCode);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void btnGetProducts_Click(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter.Fill(this.techSupport_DataDataSet.Products);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error # "+ex.Number+": "+ex.Message, ex.GetType().ToString());
            }
        }
    }
}
