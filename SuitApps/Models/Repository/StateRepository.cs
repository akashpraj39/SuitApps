using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;
using SuitApps.Models.ModelClass;

namespace SuitApps.Models.Repository
{
    public class StateRepository
    {
        #region BindState
        public static StateList BindState()
        {
            SqlConnection con;
            SqlDataReader reader;
            StateList statelist= new StateList();
            List<State> state= new  List<State>();
            WebOperationContext context = WebOperationContext.Current;
            try
            {
                con = DBconnect.getDataBaseConnection();
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetItemState", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows==true)
                {
                    while (reader.Read())
                    {
                        State State = new State();
                        State.StateID = reader["StateID"].ToString();
                        State.StateCode = reader["StateCode"].ToString();
                        State.StateParent = reader["StateParent"].ToString();
                        State.StateType = reader["StateType"].ToString();
                        State.StateName = reader["StateName"].ToString();
                        State.createdBy = reader["createdBy"].ToString();
                        state.Add(State);
                    }
                    
                }
                statelist.liststate = state;
                reader.Close();
                con.Close();

            }
            catch (Exception e)
            {

                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return statelist;
            }
            return statelist;

        }


        #endregion
        #region BindDistrict

        public static StateList BindDistrict()
        {
            SqlConnection con;
            SqlDataReader reader;
            StateList statelist = new StateList();
            List<State> state = new List<State>();
            WebOperationContext context = WebOperationContext.Current;
            try
            {
                con = DBconnect.getDataBaseConnection();
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetDistrictBind", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        State State = new State();
                        State.StateID = reader["StateID"].ToString();
                        State.StateCode = reader["StateCode"].ToString();
                        State.StateParent = reader["StateParent"].ToString();
                        State.StateType = reader["StateType"].ToString();
                        State.StateName = reader["StateName"].ToString();
                        State.createdBy = reader["createdBy"].ToString();
                        state.Add(State);
                    }

                }
                statelist.liststate = state;
                reader.Close();
                con.Close();

            }
            catch (Exception e)
            {

                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return statelist;
            }
            return statelist;

        }

        #endregion
        #region Taluk

        public static StateList BindTaluk()
        {
            SqlConnection con;
            SqlDataReader reader;
            StateList statelist = new StateList();
            List<State> state = new List<State>();
            WebOperationContext context = WebOperationContext.Current;
            try
            {
                con = DBconnect.getDataBaseConnection();
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetTalukBind", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        State State = new State();
                        State.StateID = reader["StateID"].ToString();
                        State.StateCode = reader["StateCode"].ToString();
                        State.StateParent = reader["StateParent"].ToString();
                        State.StateType = reader["StateType"].ToString();
                        State.StateName = reader["StateName"].ToString();
                        State.createdBy = reader["createdBy"].ToString();
                        State.Scategory = reader["Scategory"].ToString();

                        state.Add(State);
                    }

                }
                statelist.liststate = state;
                reader.Close();
                con.Close();

            }
            catch (Exception e)
            {

                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return statelist;
            }
            return statelist;

        }
        #endregion
    }
}