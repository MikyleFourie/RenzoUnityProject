[System.Serializable]
public class Item
{
    public string itemid;
    public string filespec;
    public string title_en;
    public string description_en;
    public string creator_en;
    public string locationCreated;
    public string dateCreated_start;
    public string type;
    public string format;
    public string medium;
    public string photographer;
    public string originalSource_url;
    public string originalSource_text;

    public Item(Item d)
    {
        itemid = d.itemid;
        filespec = d.filespec;
        title_en = d.title_en;
        description_en = d.description_en;
        creator_en = d.creator_en;
        locationCreated = d.locationCreated;
        dateCreated_start = d.dateCreated_start;
        type = d.type;
        format = d.format;
        medium = d.medium;
        photographer = d.photographer;
        originalSource_url = d.originalSource_url;
        originalSource_text = d.originalSource_text;
    }
}
