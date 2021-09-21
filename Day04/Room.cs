using System;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    public class Room
    {
        public string EncryptedName { get; private set; }
        public string Name { get; private set; }
        public int SectorID { get; private set; }
        public string CheckSum { get; private set; }
        public bool IsValid { get; private set; }

        public Room(string roomInfo)
        {
            string[] split = roomInfo.Split(new[] { "[", "]", "-" }, StringSplitOptions.RemoveEmptyEntries);
            EncryptedName = roomInfo.Substring(0, roomInfo.LastIndexOf('-'));
            CheckSum = split[split.Length - 1];
            SectorID = int.Parse(split[split.Length - 2]);

            Validate();
            DecryptName();
        }

        private void Validate()
        {
            // Create dictionary of letter frequency found in room name
            Dictionary<char, int> letters = new Dictionary<char, int>();

            foreach (var c in EncryptedName)
            {
                if (c == '-') continue;
                letters.TryGetValue(c, out int count);
                letters[c] = ++count;
            }

            // Get the top 5 letters after sorting the dictionary by Value high to low and Key A-Z
            string top5 = new string(letters.OrderByDescending(d => d.Value).ThenBy(d => d.Key).Take(5).Select(d => d.Key).ToArray());

            // Valid where the top 5 equals the Check Sum
            IsValid = top5 == CheckSum;
        }

        private void DecryptName()
        {
            char[] roomNameArray = EncryptedName.Replace("-", "").ToCharArray();

            // Loop through each letter in name array
            // Update letter by rotating through the alphabet n number of times equal to the remainder of SectorID / 26
            for (int i = 0; i < roomNameArray.Length; i++)
                for (int n = 0; n < SectorID % 26; n++)
                    roomNameArray[i] = roomNameArray[i] == 'z' ? 'a' : (char)(roomNameArray[i] + 1);

            Name = new string(roomNameArray);
        }
    }
}
