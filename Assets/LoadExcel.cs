using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    public Item blankItem;
    public List<Item> itemDatabase = new List<Item>();

    void Start()
    {
        //LoadItemData();
    }
    //public void LoadItemData()
    //{
    //    itemDatabase.Clear();

    //    //READ THE CSV FILE
    //    List<Dictionary<string, object>> data = CSVReader.Read("GAC_CSV - Origins Centre Wits University");
    //    for (int i = 0; i < data.Count; i++)
    //    {
    //        string itemid = data[i]["itemid"].ToString();
    //        //string filetype = data[i]["filetype"].ToString();
    //        string filespec = data[i]["filespec"].ToString();
    //        string title_en = data[i]["title/en"].ToString();
    //        string description_en = data[i]["description/en"].ToString();
    //        string creator_en = data[i]["creator/en"].ToString();
    //        string locationCreated = data[i]["locationCreated"].ToString();
    //        string dateCreated_start = data[i]["dateCreated:start"].ToString();
    //        //string dateCreated_end = data[i]["dateCreated:end"].ToString();
    //        //string dateCreated_display = data[i]["dateCreated:display"].ToString();
    //        //string time_period = data[i]["customtext:Time period"].ToString();
    //        string type = data[i]["type"].ToString();
    //        string format = data[i]["format"].ToString();
    //        string medium = data[i]["medium"].ToString();
    //        string photographer = data[i]["customtext:Photographer"].ToString();
    //        string originalSource_url = data[i]["originalSource:url"].ToString();
    //        string originalSource_text = data[i]["originalSource:text"].ToString();

    //        //AddItem(itemid, filetype, filespec, title_en, description_en, creator_en, locationCreated,
    //        //      dateCreated_start, dateCreated_end, dateCreated_display, time_period, type, format, medium,
    //        //      photographer, rights, originalSource_url, originalSource_text);
    //        AddItem(itemid, filespec, title_en, description_en, creator_en, locationCreated,
    //              dateCreated_start, type, format, medium,
    //              photographer, originalSource_url, originalSource_text);
    //    }
    //}

    //void AddItem(string itemid, string filespec, string title_en, string description_en, string creator_en, string locationCreated,
    //             string dateCreated_start, string type, string format, string medium,
    //             string photographer, string originalSource_url, string originalSource_text)
    //{
    //    Item tempItem = new Item(blankItem);

    //    tempItem.itemid = itemid;
    //    tempItem.filespec = filespec;
    //    tempItem.title_en = title_en;
    //    tempItem.description_en = description_en;
    //    tempItem.creator_en = creator_en;
    //    tempItem.locationCreated = locationCreated;
    //    tempItem.dateCreated_start = dateCreated_start;
    //    tempItem.type = type;
    //    tempItem.format = format;
    //    tempItem.medium = medium;
    //    tempItem.photographer = photographer;
    //    tempItem.originalSource_url = originalSource_url;
    //    tempItem.originalSource_text = originalSource_text;

    //    itemDatabase.Add(tempItem);
    //}

}