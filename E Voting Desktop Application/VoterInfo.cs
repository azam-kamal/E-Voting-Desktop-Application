namespace E_Voting_Desktop_Application
{
    internal class VoterInfo
    {
        public string VoterNicNumber { get; internal set; }
        public string VoterName { get; internal set; }
        public string VoterMobileNumber { get; internal set; }
        public string VoterHalkaNumber { get; internal set; }
        public string VoterAddress { get; internal set; }
        public bool ProvincialAssemblyVoterCast { get; internal set; }
        public bool NationalAssemblyVoterCast { get; internal set; }
    }
}