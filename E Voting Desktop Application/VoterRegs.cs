using System;

namespace E_Voting_Desktop_Application
{
    internal class VoterRegs
    {
        public DateTime VoterExpiryDate { get; internal set; }
        public string VoterId { get; internal set; }
        public string VoterMobileNumber { get; internal set; }
        public string VoterNicNumber { get; internal set; }
    }
}