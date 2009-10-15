using System;
using System.Data;
using System.Data.SqlClient;
using DataModel;

namespace DataAccessLayer
{
    public class ReviewerGateway
    {
        SqlDataAdapter adapter;
        SqlConnection connection;
        SqlCommand command;
        SqlCommandBuilder builder;

        public ReviewerGateway(SqlConnection connection)
        {
            this.connection = connection;

            command = new SqlCommand("select id, name from reviewer where id = @id",
                                     connection);
            command.Parameters.Add("@id", SqlDbType.BigInt);

            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);
        }

        public long Insert(RecordingDataSet recordingDataSet, string reviewerName)
        {
            long reviewerId = IdGenerator.GetNextId(recordingDataSet.Reviewers.TableName, connection);
            RecordingDataSet.Reviewer reviewerRow = recordingDataSet.Reviewers.NewReviewer();
            reviewerRow.Id = reviewerId;
            reviewerRow.Name = reviewerName;
            recordingDataSet.Reviewers.AddReviewer(reviewerRow);

            adapter.Update(recordingDataSet, recordingDataSet.Reviewers.TableName);

            return reviewerId;
        }

        public RecordingDataSet.Reviewer FindById(long reviewerId, RecordingDataSet recordingDataSet)
        {
            command.Parameters["@id"].Value = reviewerId;
            adapter.Fill(recordingDataSet, recordingDataSet.Reviewers.TableName);
            DataRow[] rows = recordingDataSet.Reviewers.Select(String.Format("id={0}", reviewerId));
            if (rows.Length < 1) return null;

            RecordingDataSet.Reviewer reviewer = (RecordingDataSet.Reviewer) rows[0];
            return reviewer;
        }

        public void Delete(RecordingDataSet recordingDataSet, long reviewerId)
        {
            RecordingDataSet.Reviewer loadedReviewer = FindById(reviewerId, recordingDataSet);
            loadedReviewer.Delete();
            adapter.Update(recordingDataSet, recordingDataSet.Reviewers.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            adapter.Update(recordingDataSet, recordingDataSet.Reviewers.TableName);
        }
    }
}