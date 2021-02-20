const express = require('express');
const router = express.Router();
const db = require('../../core/db');
const sql = require('mssql');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');
const { route } = require('../../app');

async function getpool() {
    const pool = await db.poolPromise;
    const result = await pool.request();
    return result;
}
//Register Voter
router.post('/register', async (req, res, next) => {
    if (req.body.voter_name != null && req.body.voter_nic != null && req.body.voter_mobile_number != null && req.body.voter_province != null && req.body.voter_city != null && req.body.voter_address != null && req.body.voter_halka_number != null && req.body.voter_national_assembly_vote_cast != null && req.body.voter_provincial_assembly_vote_cast != null && req.body.longitude != null && req.body.latitude !== null) {
        // var nic_Hash='';
        // var mobileNumber_Hash='';
        var Hash_Nic
        var Hash_Mobile
        Hash_Nic = bcrypt.hash(req.body.voter_nic, 10, async (error, generated_Nic_Hash) => {
            Hash_Mobile = bcrypt.hash(req.body.voter_mobile_number, 10, async (error, generated_Mobile_Number_Hash) => {
                const result = await getpool();
                var Cryptr = require('cryptr');
                var cryptr = new Cryptr('myTotalySecretKey');
                var encryptedNic = cryptr.encrypt(req.body.voter_nic);
                var encryptedNumber = cryptr.encrypt(req.body.voter_mobile_number);
                result
                    .input('voter_name', sql.VarChar(50), req.body.voter_name)
                    .input('voter_nic', sql.VarChar(sql.MAX), encryptedNic)
                    .input('voter_mobile_number', sql.VarChar(sql.MAX), encryptedNumber)
                    .input('voter_province', sql.VarChar(50), req.body.voter_province)
                    .input('voter_city', sql.VarChar(50), req.body.voter_city)
                    .input('voter_address', sql.VarChar(50), req.body.voter_address)
                    .input('voter_halka_number', sql.VarChar(50), req.body.voter_halka_number)
                    .input('voter_national_assembly_vote_cast', sql.Int, req.body.voter_national_assembly_vote_cast)
                    .input('voter_provincial_assembly_vote_cast', sql.Int, req.body.voter_provincial_assembly_vote_cast)
                    .input('longitude', sql.VarChar(50), req.body.longitude)
                    .input('latitude', sql.VarChar(50), req.body.latitude)
                    .output('responsemessage', sql.VarChar(50)).execute('voters_Stored_Procedure', function (error, voterData) {
                        res.status(200).json({
                            data: {
                                voterName: req.body.voter_name,
                                voterNic: encryptedNic,
                                voterMobileNumber: encryptedNumber,
                                voterProvince: req.body.voter_province,
                                voterCity: req.body.voter_city,
                                voterAddress: req.body.voter_address,
                                voterHalkaNumber: req.body.voter_halka_number,
                                voterNationalAssemblyVoteCast: req.body.voter_national_assembly_vote_cast,
                                voterProvincialAssemblyVoteCast: req.body.voter_national_assembly_vote_cast,
                                longitude: req.body.longitude,
                                latitude: req.body.latitude
                            }
                        })

                    })
            })

        })


    }

    else {

        res.status(400).json({
            Hash_Nic: generated_Nic_Hash,
            Hash_Mobile: generated_Mobile_Number_Hash,
            message: 'not found'
        })
    }
})

//Login Voter
// router.post('/login', async (req, res, next) => {
//     if (req.body.voter_nic != null && req.body.voter_mobile_number != null) {
//         const result = await getpool()
//         // console.log(encryptedNic)
//         result.execute('voters_Login_StoredProcedure', function (error, voterData) {
//             if (error) {
//                 console.log(error)
//                 res.status(500).json({
//                     message: 'Voter Not Found'
//                 })
//             }

//             else {
//                 var check = false;
//                 var Cryptr = require('cryptr');
//                 var cryptr = new Cryptr('myTotalySecretKey');
//                 for (var i = 0; i < voterData['recordset'].length; i++) {
//                     var decryptedNic = cryptr.decrypt(voterData['recordset'][i]['voter_nic']);
//                     var decryptedNumber = cryptr.decrypt(voterData['recordset'][i]['voter_mobile_number']);
//                     console.log(decryptedNic)
//                     console.log(decryptedNumber)
//                     if (decryptedNic == req.body.voter_nic && decryptedNumber == req.body.voter_mobile_number) {
//                         res.status(200).json({
//                             voterData: {
//                                 voterId: voterData['recordset'][i]['voter_Id'],
//                                 voterName: voterData['recordset'][i]['voter_name'],
//                                 voterNic: req.body.voter_nic,
//                                 voterMobileNumber: req.body.voter_mobile_number,
//                                 voterProvince: voterData['recordset'][i]['voter_province'],
//                                 voterCity: voterData['recordset'][i]['voter_city'],
//                                 voterAddress: voterData['recordset'][i]['voterAddress'],
//                                 voterHalkaNumber: voterData['recordset'][i]['voter_halka_number'],
//                                 voterNationalAssemblyVoteCast: voterData['recordset'][i]['voter_national_assembly_vote_cast'],
//                                 voterProvincialAssemblyVoteCast: voterData['recordset'][i]['voter_provincial_assembly_vote_cast'],
//                                 longitude: voterData['recordset'][i]['longitude'],
//                                 latitude: voterData['recordset'][i]['latitude'],
//                             }
//                         })
//                         check = true;
//                     }
//                 }
//                 if (!check) {
//                     res.status(400).json({
//                         message: 'Failed To Match'
//                     })
//                 }
//             }
//         })
//     }
//     else {
//         res.status(400).json({
//             message: 'not found'
//         })
//     }
// })
router.post('/login', async (req, res, next) => {
    console.log('Al Hamdullilah')
    console.log(req.body.voter_nic)
    console.log(req.body.voter_mobile_number)


    if (req.body.voter_nic != null && req.body.voter_mobile_number != null) {
        const result = await getpool()
        result.input('voter_nic', sql.VarChar(50), req.body.voter_nic).input('voter_mobile_number', sql.VarChar(50), req.body.voter_mobile_number).output('responseMessage', sql.VarChar(50), 'Success').execute('voters_Authentication_Stored_Procedures', function (error, voterData) {
            if (error) {
                res.status(500).json({
                    message: "Not Match"
                })
            }
            else {
                // console.log(voterData['output']['responseMessage'])
                if (voterData['output']['responseMessage']=='success') {
                    res.status(200).json({
                        voterId: voterData['recordset'][0]['voter_Id'],
                        voterName: voterData['recordset'][0]['voter_name'],
                        voterNic: voterData['recordset'][0]['voter_nic'],
                        voterMobileNumber: voterData['recordset'][0]['voter_mobile_number'],
                        voterProvince: voterData['recordset'][0]['voter_province'],
                        voterCity: voterData['recordset'][0]['voter_city'],
                        voterAddress: voterData['recordset'][0]['voterAddress'],
                        voterPollingStationNumber: voterData['recordset'][0]['station_Number'],
                        voterNationalAssemblyVoteCast: voterData['recordset'][0]['voter_national_assembly_vote_cast'],
                        voterProvincialAssemblyVoteCast: voterData['recordset'][0]['voter_provincial_assembly_vote_cast'],
                        longitude: voterData['recordset'][0]['longitude'],
                        latitude: voterData['recordset'][0]['latitude'],
                    })
                }
                else {
                    res.status(500).json({
                        message: "Not Match"
                    })
                }

            }
        })
    }
    else {
        res.status(400).json({
            message: 'not found'
        })
    }
})



module.exports = router
