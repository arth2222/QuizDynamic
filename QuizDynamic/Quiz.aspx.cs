using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuizDynamic
{
    public partial class Quiz : System.Web.UI.Page
    {
        private DataTable DataTableQuestions
        {
            get { return (DataTable)ViewState["Questions"]; }
            set { ViewState["Questions"] = value; }
        }
        private int Score
        {
            get { return (int)ViewState["Score"]; }
            set { ViewState["Score"] = value; }
        }
        static int QuestionId;
        static int attemptCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Score = 0;
                QuestionId = 1;
                DataTableQuestions = PopulateQuestions();
                GetCurrentQuestion(QuestionId, PopulateQuestions());
            }
        }

        protected void Next(object sender, EventArgs e)
        {
            if (QuestionId <= DataTableQuestions.Rows.Count)
            {
                if (rbtnOptions.SelectedIndex != -1)
                {
                    string otionSelected = rbtnOptions.SelectedItem.Text.Trim();
                    string correctAnswer = CorrectAnswer(QuestionId);

                    if (otionSelected.ToLower() != correctAnswer.ToLower())
                    {
                        attemptCount++;
                    }
                    else
                    {
                        attemptCount = 0;
                    }
                    if (attemptCount == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Answer is Wrong. Try Again')", true);
                        GetCurrentQuestion(QuestionId, DataTableQuestions);
                    }
                    else
                    {
                        QuestionId++;
                        if (otionSelected.ToLower() == correctAnswer.ToLower())
                        {
                            Score++;
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Answer is Correct')", true);

                        }
                        GetCurrentQuestion(QuestionId, DataTableQuestions);
                        attemptCount = 0;
                    }
                }
            }
        }

        private void GetCurrentQuestion(int questionId, DataTable dtQuestions)
        {
            if (QuestionId <= dtQuestions.Rows.Count)
            {
                lblQuestion.Text = dtQuestions.Rows[questionId - 1]["QuestionDescription"].ToString();
                hfQuestionId.Value = dtQuestions.Rows[questionId - 1]["QuestionId"].ToString();
                DataTable dtOptions = PopulateOptions(questionId);
                List<ListItem> options = new List<ListItem>();
                for (int i = 0; i < dtOptions.Rows.Count; i++)
                {
                    options.AddRange(new ListItem[] { new ListItem(dtOptions.Rows[i][1].ToString(), i.ToString()) });
                }
                rbtnOptions.Items.Clear();
                rbtnOptions.Items.AddRange(options.ToArray());
                rbtnOptions.DataBind();
            }
            else
            {
                dvQuestion.Visible = false;
                dvResult.Visible = true;
                lblResult.Text = string.Format("You Scored {0}/{1}", Score, QuestionId - 1);
            }
        }

        private DataTable PopulateQuestions()
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT QuestionId,QuestionDescription,'' AnswerSelected FROM QuestionTable", con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        con.Open();
                        da.Fill(dt);
                        con.Close();
                    }
                }
            }
            return dt;
        }

        private DataTable PopulateOptions(int questionId)
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT QuestionId,Options FROM OptionTable WHERE QuestionId = @QuestionId", con))
                {
                    cmd.Parameters.AddWithValue("@QuestionId", questionId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        con.Open();
                        da.Fill(dt);
                        con.Close();
                    }
                }
            }
            return dt;
        }

        private string CorrectAnswer(int questionId)
        {
            string answer = "";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Answer FROM AnswerTable WHERE QuestionId = @QuestionId", con))
                {
                    cmd.Parameters.AddWithValue("@QuestionId", questionId);
                    con.Open();
                    answer = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            return answer;
        }

    }
}