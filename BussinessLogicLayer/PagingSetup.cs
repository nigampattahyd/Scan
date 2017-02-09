using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BussinessLogicLayer
{
   public class PagingSetup
    {
public void LoadPagingControls(int RecordCount, int CurrentIndex, int PageSize, int totalDisplayedRows, LinkButton Prevbutton, LinkButton Nextbutton, Label lblRecCount,ref int StartPage)
        {
            int PageCount = (int)Math.Ceiling(Convert.ToDecimal(RecordCount) / Convert.ToDecimal(PageSize));
            int recStartAt = 0;
            int recEndAt = 0;

            recStartAt = ((CurrentIndex - 1) * PageSize) + 1;
            recEndAt = (((CurrentIndex - 1) * PageSize) + totalDisplayedRows);
            StartPage = recStartAt;
            if (RecordCount > PageSize)
            {
                if (CurrentIndex == 1)
                {
                    Prevbutton.Enabled = false;
                    Nextbutton.Enabled = true;
                    Nextbutton.Text = ">";
                    Prevbutton.Text = "<";
                }
                else
                {

                    if (CurrentIndex == PageCount)
                    {
                        Prevbutton.Enabled = true;
                        Nextbutton.Enabled = false;
                        Prevbutton.Text = "<";
                        Nextbutton.Text = ">";
                    }
                    else
                    {
                        if (CurrentIndex > 1)
                        {
                            Prevbutton.Enabled = true;
                            Nextbutton.Enabled = true;
                            Nextbutton.Text = ">";
                            Prevbutton.Text = "<";
                        }
                    }
                }
            }
            else
            {
                recStartAt = 1;
                if (RecordCount == 0)
                {
                    recStartAt = 0;
                }

                recEndAt = totalDisplayedRows;
                Nextbutton.Enabled = false;
                Prevbutton.Enabled = false;
                Prevbutton.Text = "<";
                Nextbutton.Text = ">";
            }

            lblRecCount.Text = "" + Convert.ToString(recStartAt) + " - " + Convert.ToString(recEndAt) + " of " + RecordCount.ToString() + "";
        }
       
        public void GetPageNo(int RecordCount, int PageSize, int ddlPageNo, Label totPG)
        {
            int PageCount = (int)Math.Ceiling(Convert.ToDecimal(RecordCount) / Convert.ToDecimal(PageSize));
            //int i = 0;
            // for (i = 1; i <= PageCount; i++)
            //{
            //    //ddlPageNo.Items.Add(new ListItem(i.ToString(), (i - 1).ToString()));
            //}
            //totPG.Text =(i - 1).ToString();
            totPG.Text = PageCount.ToString();
        }
    }
}
