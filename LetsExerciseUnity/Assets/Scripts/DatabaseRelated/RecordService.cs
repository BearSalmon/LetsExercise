using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordService
{
    DataBase dataBase;
    public RecordService(DataBase db)
    {
        dataBase = db;
    }

    public void CreateRecordTable()
    {
        //dataBase.GetRecordConnection().DropTable<Record>();
        dataBase.GetRecordConnection().CreateTable<Record>();
    }

    public int AddRecord(Record record)
    {
        return dataBase.GetRecordConnection().Insert(record);
    }

    public IEnumerable<Record> GetRecords()
    {
        return dataBase.GetRecordConnection().Table<Record>();
    }

    public Record GetRecordByNameAndDate(string name , string date)
    {
        return dataBase.GetRecordConnection().Table<Record>().Where(x => x.Name == name && x.Date == date).FirstOrDefault();
    }

    public IEnumerable<Record> GetRecordByName(string name)
    {
        return dataBase.GetRecordConnection().Table<Record>().Where(x => x.Name == name);
    }

    public int UpdateRecord(Record record)
    {
        return dataBase.GetRecordConnection().Update(record);
    }

    public int CountRecords()
    {
        return dataBase.GetRecordConnection().Table<Record>().Count();
    }
}
