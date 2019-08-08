namespace Genealogix.Records.Api.Models 
{
    public enum PersonRole 
    {
        Mother = 1,
        Father = 2,
        Newborn = 3,
        Deceased = 4,
        Witness = 5,
        Godparent = 6,
        Bride = 7,
        Groom = 8,
        Parent = 9
    }

    public enum RecordType 
    {
        Birth = 1, 
        Death = 2,
        Marriage = 3
    }
}