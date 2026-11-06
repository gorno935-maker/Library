class Reader
{
    public string Name;
    public string Surname;
    public string Phone;

    public override string ToString()
    {
        string s = Name;
        if (!string.IsNullOrEmpty(Surname)) s += " " + Surname;
        if (!string.IsNullOrEmpty(Phone)) s += $" (тел. {Phone})";
        return s;
    }

    public string FullName => string.IsNullOrEmpty(Surname) ? Name : Name + " " + Surname;
}