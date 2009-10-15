using System.Data;
using System.Data.SqlClient;
using System;
using DataModel;

namespace DataAccessLayer
{
    public class LabelGateway
    {
        private SqlDataAdapter adapter;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlCommandBuilder builder;

        public LabelGateway(SqlConnection connection)
        {
            this.connection = connection;

            command = new SqlCommand("select id, name from label where id = @id",
                connection);
            command.Parameters.Add("@id", SqlDbType.BigInt);

            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);
        }

        public long Insert(RecordingDataSet recordingDataSet, string labelName)
        {
            long labelId = IdGenerator.GetNextId(recordingDataSet.Labels.TableName, connection);
            RecordingDataSet.Label labelRow = recordingDataSet.Labels.NewLabel();
            labelRow.Id = labelId;
            labelRow.Name = labelName;
            recordingDataSet.Labels.AddLabel(labelRow);

            adapter.Update(recordingDataSet, recordingDataSet.Labels.TableName);

            return labelId;
        }

        public RecordingDataSet.Label FindById(long labelId, RecordingDataSet recordingDataSet)
        {
            command.Parameters["@id"].Value = labelId;
            adapter.Fill(recordingDataSet, recordingDataSet.Labels.TableName);
            DataRow[] rows = recordingDataSet.Labels.Select(String.Format("id={0}", labelId));
            if (rows.Length < 1) return null;

            RecordingDataSet.Label label = (RecordingDataSet.Label)rows[0];
            return label;
        }

        public void Delete(RecordingDataSet recordingDataSet, long labelId)
        {
            RecordingDataSet.Label loadedLabel = FindById(labelId, recordingDataSet);
            loadedLabel.Delete();
            adapter.Update(recordingDataSet, recordingDataSet.Labels.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            adapter.Update(recordingDataSet, recordingDataSet.Labels.TableName);
        }
    }

}
