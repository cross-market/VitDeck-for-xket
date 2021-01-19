using System.Linq;

namespace VitDeck.Validator
{
    public static class XketAssetData
    {
        public static string[] GUIDs = Vket5OfficialAssetData.GUIDs.Concat(new[]
        {
            #region ゲーミング波紋シェーダー
            "678cdccda61385b4fa012a33b9183b53",
            "837a3cbdac0378e43a33c8fe66d31250",
            "0167df56538378a43b4a733f5a152d52",
            "a02bed6b566d9164b9ed56ddb8889b22",
            "59b5423c83501554080ddecda0d88400",
            "f6344b557c865f740ba02e0ee82b60e8",
            "5f94a00059794d8469e1ec8b94e48a09",
            "bc6048cc78f66194a8e162f2d79ba974",

            #endregion

            #region NamakoXket2Shader.unitypackage
            "6e06ec2e5359bc044a2187d0071d3dba",
            "10fec87cd50d79c4c8fcc04d81d74141",
            "4709c495f53a23d4fae32ab452cda011",
            "30d609dc4d65be34599e9cb0a9df6dff",
            "e3c24b0000abac044ae1aab455d7256a",
            "ef66e9958b88aae4aaeae069759a20a8",
            "c06303d38bec91b4a9f4577574371eb3",
            "eceaa9f87b7c73745b543355917bd7ce",
            "cd3be97009175464e906dbca1a60c7ac",
            "2bad451a64f7a7f41a4271dbeec9f1fe",
            "1fe14209bb3638d40a8ea3d902a08387",
            "a218b31773bba334290791f447e9fbbf",
            "8affc986b6861b44b89ee55df9ef018e",
            "4889730dec3cad9428dd5467e31f1adf",
            "9c03ee8d35db3f2408e6491c64becebf",
            "0b3dbe8007dd0d749a3e8cf6c66f28bd",
            "cec8b6ff2c6ebb94b98082fdced52a6e",
            #endregion

            #region noribenQuestWaterシェーダー
            "12d05ea3116dd364cb401c318a753912",
            "50ce8b1b6d9112243aaf6fb4e39635f5",
            "a8380f17eb815f740b8f070b0af2edaa",
            "92e4b24d71db4e946a2fbbf4ab519c4d",
            "f37658f453acdef42be51fc5a26d9bb2",
            "e78e27bb74f7ba34cbfc1b6bcc5fb67d",
            "6ae273697d288074d9257f8a66afd052",
            "e4e7c92103289a04db55f7cd35ea7f3f",
            "b05e8486f2188774296b0e7fecc5ca33",
            "4660d9c95eb9e14448132e3c6060918b",
            "27798e884c23c71419c98c6c2053ddb8",
            "4e316c84b6113124585c49b7601495c1",
            "eacfe4d521dedb943ad40be45ad37414",
            "8c235d73d794d7240ae0e60873593580",
            "c8c2befe2310980488c5b12442951599",

            #endregion

            #region text-template
            "02567ef9c767fa941a9cffaabc25c635",
            "d430eeeaf818b3544ad140d4c03d4e64",
            "32e1a70dd7f6b3d4dbf59f88f57276f6",
            "691ad3e9b2fd50243b327ef42e5bbd9b",
            "52d6b00ad242d41419f549e802f7be60",
            "36371dcb50975dc49b6244b4e871ce93",
            "d3bf680baa6baac44b5f8eba8edb49a1",
            "c88fabbf0a8f42446b22b70f204bf7ec",
            "26c26099a0765d943863409f4a539669",
            "0f29c5d8d1d3cf1408f8c287949700bf",
            "3d804a353ae10334abbd7d946703314a",
            "28a77ed9794d04d4ca69d1dba6c0f545",
            "9327bb9376788884f948037daac9cabc",
            "e55918467baa21041ba5f5d4ce5a5c17",
            #endregion

            #region Oculus LipSync
            "9672c993270b3764a9739e1065b5d67a",
            "a671bd453c0b47b438476dd2ce40b6cd",
            "404b6b1044f864c83becc58f44a8dff5",
            "7b2d242d170b44fc3b3a1b9ba4adc0a7",
            "3e1d42ccc29c54bf68636db8acf2eed8",
            "76b873b07f695cb4cb6515ecf670c814",
            "e21cea670b0f32c428ee862e590a7108",
            "1ccea2acd443f4ad4bb9a9f33fc960c0",
            "7152fa4bbe0044f12886bfb6274626bf",
            "73f38d977e4ed45f48b632a00fb9c579",
            "a7d01b0eb149d9945a23728e5c7f5fcb",
            "08b0ff764adc142d79bfae7dd917cc16",
            "c2d166626e0b34a40b774467cc6c5868",
            "fe1e90827fcfcac4883e383c51a78ddd",
            "196133c6d4044404698c27b5d1724e2a",
            "cbaf1c32e7ec84f10a51be1b09c0ef46",
            "e59519b819b1843cb94acf4e281e8c45",
            "466d221c743ab400a8318afe7b830593",
            "e2d7f58dd4c84431b994602bf58090de",
            "5df7ce8a60c93cc4e888052b7977b6a9",
            "dab49ccccea81c4469fd0901d806845b",
            "fd2bb13164cc64a2087f565d8c66af4f",
            "e906d4c6bf3304d5dac06eee4fe24c54",
            "d0455a0ebc21e4c758037cb4b82bebd2",
            "c11eea25db1a81c429314ae5ac32dc48",
            "76f25b997543f432bbe00737485b392e",
            "0ad01ecbcffbb4c91af7ffa1ebc14f74",
            "3d380053edac948aa9d428fce6a5288b",
            "a4624e55b9bd82444bf4687fee7e9346",
            "5c06354455999f94ea26114e261dde6d",
            "5e6531e8f56b42547b8d2ddf7a7363d1",
            "ffbd7db31d3ad6e4a97a2f312babdcb6",
            "29a06a0eefee1b542a7c286f1689ffab",
            "5563aa34057c347499c480ea33c5d593",
            "49ba9bfa609aed54ea6d5d1c1390bc15",
            "ad60c8114191fdf41aa0ea64e132add9",
            "ba0b9f69af91c5f46b490346e5552136",
            "9c21cf8c4dffca9418cebd0a578972b4",
            "5f658e6e48970d340b4491a5fc959251",
            "660be1fdb8590b14bab012a991bdeb86",
            "bea5b996ab6235c4aaa1be9b11aea8c4",
            "f82b1e3c015abaa409d91ab8eb628ac4",
            "6d47a54f2b89a59459fd7561d360bab2",
            "46b2920291e9b8c4888ad3fe3f5e5e69",
            "31e6326c0bd6143479322c5b4a5fe949",
            "d14bb80824ed25b44a65ddfc7f591e29",
            "6c05c00fcd925e1469ab8cab6316329f",
            "b8f002f88e9d7e847b5306f3338f80db",
            "5290b3a55eaa1b7458665dc2d856c042",
            "0cd9cc8c2d1778943869a1c67c9da38f",
            "bbaa933ce12896e4eb67e30b96d795e9",
            "1be7ff64e219f4e44b17e7a42f13247b",
            "c1eb8c88592ff744ca29ebf826bb58e1",
            "57850b02365448041958df056264b3e3",
            "f3ffa7a9d7a87466691fa59d187d0f5a",
            "f5b1c127a92bb426f8f5636c5ea8ab34",
            "7bdb12c9252924dd0bf9d2809e01adaf",
            "a6504b8c29dd76d4388eef4c5458c108",
            "79c2e7f7b1a214443ab64ef151d6bd36",
            "1c5d549bd7e8f2142b88f679c5b3d73e",
            "cffe8c2b7142fa3438fc1b6ccaeda372",
            "4b22c2612a8292646806a50f38127837",
            "e4c63fc874ed2ed42b7808b1a310238a",
            "4dd277bd9572488489906165e0931952",
            "0d336f164a5a4454db3960b2f9fc7a85",
            "8dc55266323c0ad439967b2975af7840",
            "e63268aff72466742ada144924d3f899",
            "0a5206cd53b21be4588ef635952929e9",
            "a59ddab39a70b984f94af4f1ceb261d2",
            "50cd36abe38c57646bcd09be2353f905",
            "8a6c32d06b48d244b9c65729404d2afe",
            "c62f9b5ef85d22f449e72c67c610059d",
            "a96f219c252e0cc4eb074d5b7bbda9b3",
            "82aa5cb7a870de440baadff9083be41c",
            "f43c520a9bad8a3489109c869f454576",
            "c0d528b758a004fcaac677043e8de6ad",
            "e073e338e215b4ae9a7fcdf6891e7955",
            "b0b97b38f2f1fd24185315141a6c6a56",
            "bc30a49f30010eb42a8b59ec685eac57",
            "02d5ed157083b494e85013bad8fd5e12",
            "354250b5dc6a14f49b541724e9dd3c37",
            "27d84f95a4766db44a26aea09cc67373",
            "c60ad94815c68aa41a786306cd588495",
            "7537accd8e2c6024c860b20e3e7e3424",
            "edde1cb2a78471f409fce5084e6c720c",
            #endregion

            #region PhysSound
            "516e3a4ab7529d24caf2243b8c0c481e",
            "d27998a9f2d1ea34da750fd71ef1ea1c",
            "2f72b921183954c41b6ac01ceac16ccc",
            "f844d0d6aec23a243abd2ac9ee2c77ec",
            "0b58b277751de7644a9d029eeac88990",
            "c21e10a0ceb7c9d419a24c5a7a294491",
            "93939afa1cbabdb498f30fb8b7da4592",
            "0c31a13c15798d94aac153097f821cd2",
            "23ae4d21aaedad74d9233241c9411ec6",
            "71b2d33b531c8fd4ba606bd1b6323a78",
            "e3fd64d5e2ad635468e8104dd9f7ec2f",
            "911a10cbaefb55248a823c7461002714",
            "be01abacc6be0c1428144758e8653c72",
            "a59d9ac9ac2b53043b7d54871ae2e06f",
            "6b8a72627fc65c341885a56706edd829",
            "dc6cf0b37012d484893a37dfc27170e9",
            "a164cdb3838286449af1a88c75896674",
            "4312855ac381b914f9558f9b42c69504",
            "1e71e50ad3547284f9237aa2fc872b9a",
            "0e0e518e4d00b3448bf0fc5509ab493f",
            "c995e6614677d594691c76fafdd6ca2a",
            "927125c4662156b4cbc20609a2346c18",
            "bdac02d039e6c2f4dae6e3e57d2da17d",
            "07842165f0453464aab2d1894d79deca",
            "6782dcc302077e24f814003b9956fd5f",
            "93ae0b59410fee64abe696fe568cc558",
            "51b1186b78d34404cb8f8b26c18eb35d",
            "a7af3440b48952d4c9add9b8bbd05fdf",
            "dae91f503d4b06f45962baeefd994352",
            "4ef586fd4e4d3f14a93850ef81ab965d",
            "94dc3627b2a5424468fd0a29a45eccb3",
            "7b1edfa54a30cc14c80480c6c21ab6af",
            "880bbe89bb329404bbfd54395efe5fc9",
            "dc381ff9aee31ba4485b519cd21cd0bb",
            "0ae5895f39eaec3489f46b74be48fe8a",
            "07c9db3aa6451ba448b7e2312a40ec1a",
            "d1af5ba5cf63ccd4691b9165d0e2743e",
            "e271dd0dd4db34c4399362eedc9eb814",
            "f9b58bc297d651843bca2cb720a743ca",
            "7e06b4ddba04bdd4282fabf3bac913f1",
            "212908ab48c469246a1fa3c110d3ff0f",
            "77711f677035ce640998fd0811305b5f",
            "c728d5ae1fc69a34cacebe7ab17b6d00",
            "35eaf9bc12ac83d4cbb64e0b7b559154",
            "43fbea5a4fa39914caf87583c9f0a215",
            "55e29cfc82dd7fd4eb76c2bd278b4c2a",
            "4c4aba3def01a094d91403627e8c2590",
            "52bbe199a4a7c794499dbb74f2d0ea51",
            "92e1f5d43019ce34fb640a8f8b80675a",
            "887f648090ad1814ca29c755e388e761",
            "e9b2ebc743f64f54e88a6d7b0c22735c",
            "24d70d8d5dd91ce4788dffa0b8d52499",
            "5c099903d219d5e4894f2fceb00794c7",
            "a64db8d64c5ee9846b2fec1892b2a82f",
            "94619f80dd09282429d4923bd1353207",
            "ea477303edc05924e9ac2b601b8d21ab",
            "6898ead3978f3f24abde360099bc5ad2",
            "26310e1685b61dd49949544ac7b81735",
            "00c6aca58d785ce41b7e5f77205818cd",
            "8235cc474e2aa644887d4bd26e664729",
            "bd68277d513e5474b9c8b3422406cec0",
            "ef3e8320fd4310747a758671194f5681",
            "9a532385e5f2c8b4fbd8a76c22fa5862",
            "427ea2c8918d7ef4b94cf59013ae59eb",
            "a6f70ffd42221a347a842d7fb84d02c1",
            "694a53fae45622f4c8d2269c4d647399",
            "037d5049bd713dd448f545820951fe1d",
            "e5c1f2ea7a813a7408be7faed3e6967e",
            "edec9285c162b2b42aa839316ac69bfd",
            "980bc450974b2f747943e6819b67d206",
            "3b61dca4647f61f4f9051378fa810e5c",
            "1bb4e41ab7a6e9e449b95085d9c8cbb8",
            "421ed7e0506406741a0aef0d6e3ef3eb",
            "2602c211fbeb62d4e9503d67d85bcb39",
            "dc3b059c7c4ee6f4c9ddc7802e1df77a",
            "23d13f52b5a01964e82ea27791642175",
            "37dc13e7cd58a0c42a0ba1ebbc1a5175",
            "5e00459b4da2cb944bf653bf97849419",
            "db9b5d1b73592c84483d812421922982",
            "57d16b0b78483ab41b8e3df634f1e223",
            "b39000c375328374aac59ca984ec0ada",
            "8d4b635fea136904f9c0f437997a6928",
            "3ed8a64dd8cd0024eb58e1dea09ed8df",
            "491955361a775dd48af1c1cf0f8c593b",
            "d1a46b85acec488479294b28044c0b36",
            "ddb9295558debd040818c61f2f32bdc5",
            "7148ab476afc6254489966e0e8058a40",
            "153334e5978d5554ba1cb364e1de3e57",
            "7ab2dae0165ff71449b26623bdb9298e",
            "38c5d2aa4ec230a48a6528251486010a",
            "2c99c62bd3cd5874cbb4de411e40b4a8",
            "78d7742e5be23fe41aa89d5ba4e82589",
            "4d21af99d5fe64949a113308138b8c45",
            "f464a4fb36175d849a9a8079f7279ffc",
            "0b1e4a817bdc70146bb52d4bb337ea8e",
            "34451acac578b4f48abbf7e03b23278b",
            "939a77ca1a9770d48adcdedac0a107ea",
            "91c0dc3ffa247314b8af35444e57e413",
            "faf106e9a3829d74baf962938d1875f8",
            "41a3dd51253c37d46ab6ce54bba685f5",
            "4697a3cc5a2abfd489fe746ed2509cf4",
            "84968e40525482047959b1fe1d6b8950",
            #endregion

            #region Unlit_WF_ShaderSuite_20210101_Core.unitypackage
            "9bb25feaa2a612d4f8016e2dff545f95",
            "b8bbbd51c2e41dd4bbcb0da1b7a48808",
            "4ebc920fe2745624bbed02e79a222e3d",
            "3a6edc45e5f76964c9679f43d3839c28",
            "b71e250f3c9f9a54cac228148bc800f7",
            "6b1a45934e0846141979f322772dc3b8",
            "052a5a21704733543a9cbbf6369ca43c",
            "3ca4c3d3a4488214db9818862a2eff69",
            "4f0275352c196ca4d864b6611897bfd7",
            "e3269783b9ab81e4f85d813345bc1a7e",
            "975bd523d795c144fab0af1c55f7b20e",
            "2a4dc116efeb0db4192f11f17d555b87",
            "c02ebf9b7a5d66c4ead5f94ef99b20c8",
            "54ed4f64546b23741987a94ff9769567",
            "b8e19d3beb8c169458f9b150a00f40ec",
            "a6e5dce22330ea542a4cfbb7031863da",
            "c7e5995223250464cac205689e058693",
            "58bb80b63bec29d4384e105c53ca6970",
            "2210f95a2274e9d4faf8a14dac933fdb",
            "c0f75d3ed420fd144a74722588d3bc21",
            "21f6eaa1dd1f25c4cb29a42c4ff5d98f",
            "4ba701b07ccc81e4aae7f053bf332eab",
            "f3f80636c64e389498b3b19e2ee218da",
            "90cac9ec3b2a7524eb99b36ab87f25f1",
            "871fd7a51a8ea3e4980c3fe7b8347619",
            "58ccf9c912b226146a25726b8a1f04db",
            "af51615040dcdad4cb01c29ea34dbb03",
            "4bd76f6599a5b8e4d88d81300fb74c37",
            "d279a88eda1ae0e4c89e92539639eb16",
            "e0b93fdad2eeedf42baccbc0975cdd1d",
            "af3422dc9372a89449a9f44d409d9714",
            "0a7a6cdca16a38548a5d81aca8d4e3ba",
            "4e4be4aab63a2bd4fbcea2390ae92fdf",
            "a3678756e883b9349ac22fce33313139",
            "4eef00f52cc21b04e9e34e4caefa6bbf",
            "64bf3ca653a7b274fab3e8a87016bfb0",
            "660abd485057f4740ac9050f7ab3237d",
            "a5ae7f40ac53e274ea0bc1262e1f6895",
            "ab4eb87c406a22f46887cf72178e2685",
            "5523e041d29d259439fa14bd131f5c82",
            "5498b01615002d948bea7542f55e0c07",
            "9350854c6e88f3f4eb873d2f94ff3328",
            "ad88000744b4fb241835ba6ec106caf4",
            "0733cfc88032e8d4eafce250263c497c",
            "2cf66b0706c40744baab089297afa895",
            "747bf283d686334469fb662b2fc4a5c2",
            "d242cb83664caae4f957030870dd801d",
            "dd3a683002b3a6f43bdb6c97bd0985c1",
            "94ee7f8988740fd4887f8b1ce41f0c1c",
            "3bde56820d1aece41bd22966876a061c",
            "78d2e3fa0b8eb674aa9cf9e048f79c93",
            "8c7888a4ac175584f81e0b6e7d4af5a7",
            "15212414cba0c7a4aac92d94a4ae8750",
            "d1e7b0a18e221a1409ad59065ec157e4",
            "2efe527cfcbf0e1408b67463225f552f",
            "0b53cf0bcd0f9db4fa9d1297d255d06d",
            "d01a5c313ada49e488b2ef8c6b00f56d",
            "a220e3e0675cc3f4f817a485688788d6",
            "2d294f328149d944eb0899b452ff879c",
            "1435581bcf13e7a47b5bf5636f8d8252",
            "e7263331a8ee0a04aa4a271fc1fef104",
            "0299954f2a9b0994f8c9587945948766",
            "06e9294a93df4474cac2f4157b5e1d1d",
            "dfb821bc7afadc14591e4338a8ec865f",
            "0380b1621ab524c43aeb10eba3346ea6",
            "578346e318940304389ae3dda992ac86",
            "2762fae01792d2745ad5d02376392d52",
            "ef1a901a2feeb0a45859ecc184e2e3e2",
            "b892a7ae3359eb0428b2f8aebf24d314",
            "45af0d16a1af0a947b445e08dd6dead4",
            "34a1cdb7cd82cd045a521aa2db90ba27",
            "77ee5292cc4f46649a13611c8d76c85b",
            "e33666b113c868d41bfa058f5bc50d9c",
            "be668f2e5a5e4ef46838001f79babcef",
            "22546fe6fb0bed84e8db3fc80b0b2302",
            "93e68367384c3bd42a3a37868cc25554",
            "8e439fa11883d4b429904a7fc398851e",
            "afa8b2842288b044b9cdccd7508670a7",
            "074195645f64a224d9482cb666563c89",
            "bf91baf439ae72542bd718eb51378f5a",
            "ad9922cd501663b4cbfbef594d1b22d0",
            "95ae3c73098e55148862b3125c46785e",
            "bad784f674c77404f8234c8d284656d2",
#endregion
        }).ToArray();
    }
}
