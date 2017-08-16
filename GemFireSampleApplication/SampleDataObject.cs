using System;

namespace GemFireSampleApplication
{
    public class SampleDataObject
    {
        private String firstName;
        private String lastName;
        private int id;

        public SampleDataObject()
        {
        }
        public SampleDataObject(String firstName, String lastName, int id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
        }
        public String FirstName
        {
            get; set;
        }
        public String LastName
        {
            get; set;
        }
        public int ID
        {
            get; set;
        }
        public override string ToString()
        {
            return "{ \"firstName\" : \"" + firstName + "\",  \"lastName\" : \"" + lastName + "\", \"id\" : " + id + " }";
        }
    }
}
