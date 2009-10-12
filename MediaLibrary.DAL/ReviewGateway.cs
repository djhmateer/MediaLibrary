using System;
using System.Data;
using System.Data.SqlClient;

namespace MediaLibrary
{
    public class ReviewGateway
    {
        SqlCommand reviewCommand;
        SqlCommandBuilder reviewBuilder;
        SqlConnection connection;
        SqlDataAdapter reviewAdapter;
        SqlCommand findByRecordingIdCommand;
        SqlDataAdapter findByRecordingIdAdapter;

        public ReviewGateway(SqlConnection connection)
        {
            this.connection = connection;

            reviewCommand =
                new SqlCommand("select id, reviewerId, recordingId, rating, review from review where id = @id",
                               connection);
            reviewCommand.Parameters.Add("@id", SqlDbType.BigInt);

            reviewAdapter = new SqlDataAdapter(reviewCommand);
            reviewBuilder = new SqlCommandBuilder(reviewAdapter);

            findByRecordingIdCommand = new SqlCommand("select id, reviewerId, recordingId, rating, review from review where recordingId = @recordingId",
                                                      connection);
            findByRecordingIdCommand.Parameters.Add("@recordingId", SqlDbType.BigInt);
            findByRecordingIdAdapter = new SqlDataAdapter(findByRecordingIdCommand);
        }

        public long Insert(RecordingDataSet recordingDataSet, int rating, string content)
        {
            long reviewId = IdGenerator.GetNextId(recordingDataSet.Reviews.TableName, connection);
            RecordingDataSet.Review reviewRow = recordingDataSet.Reviews.NewReview();

            reviewRow.Id = reviewId;
            reviewRow.Rating = rating;
            reviewRow.Content = content;

            recordingDataSet.Reviews.AddReview(reviewRow);

            reviewAdapter.Update(recordingDataSet, recordingDataSet.Reviews.TableName);

            return reviewId;
        }

        public RecordingDataSet.Review
            FindById(long reviewId, RecordingDataSet recordingDataSet)
        {
            reviewCommand.Parameters["@id"].Value = reviewId;
            reviewAdapter.Fill(recordingDataSet, recordingDataSet.Reviews.TableName);
            DataRow[] rows = recordingDataSet.Reviews.Select(String.Format("id={0}", reviewId));
            if (rows.Length < 1) return null;

            RecordingDataSet.Review review = (RecordingDataSet.Review) rows[0];
            return review;
        }

        public RecordingDataSet.Review[]
            FindByRecordingId(long recordingId, RecordingDataSet recordingDataSet)
        {
            findByRecordingIdCommand.Parameters["@recordingId"].Value = recordingId;
            findByRecordingIdAdapter.Fill(recordingDataSet, recordingDataSet.Reviews.TableName);
            DataRow[] rows =
                recordingDataSet.Reviews.Select(
                    String.Format("recordingId={0}", recordingId));

            return (RecordingDataSet.Review[]) rows;
        }

        public void Delete(RecordingDataSet recordingDataSet, long reviewId)
        {
            RecordingDataSet.Review loadedReview = FindById(reviewId, recordingDataSet);
            loadedReview.Delete();
            reviewAdapter.Update(recordingDataSet, recordingDataSet.Reviews.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            reviewAdapter.Update(recordingDataSet, recordingDataSet.Reviews.TableName);
        }
    }
}