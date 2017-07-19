namespace Excercise05
{
    class TagTransformer
    {
        public static string Transform(string tag)
        {
            if (!tag.StartsWith("#"))
            {
                tag = "#" + tag;
            }
            if (tag.Contains(" "))
            {
                tag = tag.Replace(" ", string.Empty);
            }
            if (tag.Contains("\t"))
            {
                tag = tag.Replace("\t", string.Empty);
            }
            if (tag.Length > 20)
            {
                tag = tag.Substring(0, 20);
            }

            return tag;
        }
    }
}
