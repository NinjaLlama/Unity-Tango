// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
//
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 /// <summary>
/// Copyright (C) 2015-2017 Regents of the University of California.
/// </summary>
///
namespace net.named_data.jndn.tests.integration_tests {
	
	using ILOG.J2CsMapping.NIO;
	using ILOG.J2CsMapping.Util;
	using ILOG.J2CsMapping.Util.Logging;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using System.spec;
	using javax.crypto;
	using net.named_data.jndn;
	using net.named_data.jndn.encoding;
	using net.named_data.jndn.encoding.der;
	using net.named_data.jndn.encrypt;
	using net.named_data.jndn.encrypt.algo;
	using net.named_data.jndn.security;
	using net.named_data.jndn.security.identity;
	using net.named_data.jndn.security.policy;
	using net.named_data.jndn.util;
	
	public class TestProducer {
		public TestProducer() {
			this.decryptionKeys = new Hashtable();
			this.encryptionKeys = new Hashtable();
		}
	
		// Convert the int array to a ByteBuffer.
		public static ByteBuffer toBuffer(int[] array) {
			ByteBuffer result = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(array.Length);
			for (int i = 0; i < array.Length; ++i)
				result.put((byte) (array[i] & 0xff));
	
			result.flip();
			return result;
		}
	
		private static readonly ByteBuffer DATA_CONTENT = toBuffer(new int[] { 0xcb,
				0xe5, 0x6a, 0x80, 0x41, 0x24, 0x58, 0x23, 0x84, 0x14, 0x15, 0x61,
				0x80, 0xb9, 0x5e, 0xbd, 0xce, 0x32, 0xb4, 0xbe, 0xbc, 0x91, 0x31,
				0xd6, 0x19, 0x00, 0x80, 0x8b, 0xfa, 0x00, 0x05, 0x9c });
	
		public void setUp() {
			// Don't show INFO log messages.
			ILOG.J2CsMapping.Util.Logging.Logger.getLogger("").setLevel(ILOG.J2CsMapping.Util.Logging.Level.WARNING);
	
			FileInfo policyConfigDirectory = net.named_data.jndn.tests.integration_tests.IntegrationTestsCommon
					.getPolicyConfigDirectory();
			databaseFilePath = new FileInfo(System.IO.Path.Combine(policyConfigDirectory.FullName,"test.db"));
			databaseFilePath.delete();
	
			// Set up the key chain.
			MemoryIdentityStorage identityStorage = new MemoryIdentityStorage();
			MemoryPrivateKeyStorage privateKeyStorage = new MemoryPrivateKeyStorage();
			keyChain = new KeyChain(new IdentityManager(identityStorage,
					privateKeyStorage), new NoVerifyPolicyManager());
			Name identityName = new Name("TestProducer");
			certificateName = keyChain.createIdentityAndCertificate(identityName);
			keyChain.getIdentityManager().setDefaultIdentity(identityName);
		}
	
		public void tearDown() {
			databaseFilePath.delete();
		}
	
		internal void createEncryptionKey(Name eKeyName, Name timeMarker) {
			RsaKeyParams paras = new RsaKeyParams();
			eKeyName = new Name(eKeyName);
			eKeyName.append(timeMarker);
	
			Blob dKeyBlob = net.named_data.jndn.encrypt.algo.RsaAlgorithm.generateKey(paras).getKeyBits();
			Blob eKeyBlob = net.named_data.jndn.encrypt.algo.RsaAlgorithm.deriveEncryptKey(dKeyBlob).getKeyBits();
			ILOG.J2CsMapping.Collections.Collections.Put(decryptionKeys,eKeyName,dKeyBlob);
	
			Data keyData = new Data(eKeyName);
			keyData.setContent(eKeyBlob);
			keyChain.sign(keyData, certificateName);
			ILOG.J2CsMapping.Collections.Collections.Put(encryptionKeys,eKeyName,keyData);
		}
	
		public void testContentKeyRequest() {
			Name prefix = new Name("/prefix");
			Name suffix = new Name("/a/b/c");
			Name expectedInterest = new Name(prefix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_READ);
			expectedInterest.append(suffix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_E_KEY);
	
			Name cKeyName = new Name(prefix);
			cKeyName.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_SAMPLE);
			cKeyName.append(suffix);
			cKeyName.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_C_KEY);
	
			Name timeMarker = new Name("20150101T100000/20150101T120000");
			double testTime1 = net.named_data.jndn.encrypt.Schedule.fromIsoString("20150101T100001");
			double testTime2 = net.named_data.jndn.encrypt.Schedule.fromIsoString("20150101T110001");
			Name.Component testTimeRounded1 = new Name.Component(
					"20150101T100000");
			Name.Component testTimeRounded2 = new Name.Component(
					"20150101T110000");
			Name.Component testTimeComponent2 = new Name.Component(
					"20150101T110001");
	
			// Create content keys required for this test case:
			for (int i = 0; i < suffix.size(); ++i) {
				createEncryptionKey(expectedInterest, timeMarker);
				expectedInterest = expectedInterest.getPrefix(-2).append(
						net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_E_KEY);
			}
	
			int[] expressInterestCallCount = new int[] { 0 };
	
			// Prepare a LocalTestFace to instantly answer calls to expressInterest.
			
	
			TestProducer.LocalTestFace  face = new TestProducer.LocalTestFace (this, timeMarker,
					expressInterestCallCount);
	
			// Verify that the content key is correctly encrypted for each domain, and
			// the produce method encrypts the provided data with the same content key.
			ProducerDb testDb = new Sqlite3ProducerDb(
					databaseFilePath.FullName);
			Producer producer = new Producer(prefix, suffix, face, keyChain, testDb);
			Blob[] contentKey = new Blob[] { null };
	
			
	
			TestProducer.CheckEncryptionKeys  checkEncryptionKeys = new TestProducer.CheckEncryptionKeys (
					this, expressInterestCallCount, contentKey, cKeyName, testDb);
	
			// An initial test to confirm that keys are created for this time slot.
			Name contentKeyName1 = producer.createContentKey(testTime1,
					new TestProducer.Anonymous_C4 (checkEncryptionKeys, testTime1,
							testTimeRounded1));
	
			// Verify that we do not repeat the search for e-keys. The total
			//   expressInterestCallCount should be the same.
			Name contentKeyName2 = producer.createContentKey(testTime2,
					new TestProducer.Anonymous_C3 (testTimeRounded2, checkEncryptionKeys,
							testTime2));
	
			// Confirm content key names are correct
			Assert.AssertEquals(cKeyName, contentKeyName1.getPrefix(-1));
			Assert.AssertEquals(testTimeRounded1, contentKeyName1.get(6));
			Assert.AssertEquals(cKeyName, contentKeyName2.getPrefix(-1));
			Assert.AssertEquals(testTimeRounded2, contentKeyName2.get(6));
	
			// Confirm that produce encrypts with the correct key and has the right name.
			Data testData = new Data();
			producer.produce(testData, testTime2, new Blob(DATA_CONTENT, false));
	
			Name producedName = testData.getName();
			Assert.AssertEquals(cKeyName.getPrefix(-1), producedName.getSubName(0, 5));
			Assert.AssertEquals(testTimeComponent2, producedName.get(5));
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_FOR, producedName.get(6));
			Assert.AssertEquals(cKeyName, producedName.getSubName(7, 6));
			Assert.AssertEquals(testTimeRounded2, producedName.get(13));
	
			Blob dataBlob = testData.getContent();
	
			EncryptedContent dataContent = new EncryptedContent();
			dataContent.wireDecode(dataBlob);
			Blob encryptedData = dataContent.getPayload();
			Blob initialVector = dataContent.getInitialVector();
	
			EncryptParams paras = new EncryptParams(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.AesCbc,
					16);
			paras.setInitialVector(initialVector);
			Blob decryptTest = net.named_data.jndn.encrypt.algo.AesAlgorithm.decrypt(contentKey[0], encryptedData,
					paras);
			Assert.AssertTrue(decryptTest.equals(new Blob(DATA_CONTENT, false)));
		}
	
		public void testContentKeySearch() {
			Name timeMarkerFirstHop = new Name("20150101T070000/20150101T080000");
			Name timeMarkerSecondHop = new Name("20150101T080000/20150101T090000");
			Name timeMarkerThirdHop = new Name(
					"20150101T100000/20150101T110000");
	
			Name prefix = new Name("/prefix");
			Name suffix = new Name("/suffix");
			Name expectedInterest = new Name(prefix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_READ);
			expectedInterest.append(suffix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_E_KEY);
	
			Name cKeyName = new Name(prefix);
			cKeyName.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_SAMPLE);
			cKeyName.append(suffix);
			cKeyName.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_C_KEY);
	
			double testTime = net.named_data.jndn.encrypt.Schedule.fromIsoString("20150101T100001");
	
			// Create content keys required for this test case:
			createEncryptionKey(expectedInterest, timeMarkerFirstHop);
			createEncryptionKey(expectedInterest, timeMarkerSecondHop);
			createEncryptionKey(expectedInterest, timeMarkerThirdHop);
	
			int[] requestCount = new int[] { 0 };
	
			// Prepare a LocalTestFace to instantly answer calls to expressInterest.
			
	
			TestProducer.LocalTestFace2  face = new TestProducer.LocalTestFace2 (this, expectedInterest,
					timeMarkerFirstHop, timeMarkerSecondHop, timeMarkerThirdHop,
					requestCount);
	
			// Verify that if a key is found, but not within the right time slot, the
			// search is refined until a valid time slot is found.
			ProducerDb testDb = new Sqlite3ProducerDb(
					databaseFilePath.FullName);
			Producer producer = new Producer(prefix, suffix, face, keyChain, testDb);
			producer.createContentKey(testTime, new TestProducer.Anonymous_C2 (expectedInterest, requestCount, cKeyName,
					timeMarkerThirdHop));
		}
	
		public void testContentKeyTimeout() {
			Name prefix = new Name("/prefix");
			Name suffix = new Name("/suffix");
			Name expectedInterest = new Name(prefix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_READ);
			expectedInterest.append(suffix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_E_KEY);
	
			double testTime = net.named_data.jndn.encrypt.Schedule.fromIsoString("20150101T100001");
	
			int[] timeoutCount = new int[] { 0 };
	
			// Prepare a LocalTestFace to instantly answer calls to expressInterest.
			
	
			TestProducer.LocalTestFace3  face = new TestProducer.LocalTestFace3 (expectedInterest, timeoutCount);
	
			// Verify that if no response is received, the producer appropriately times
			// out. The result vector should not contain elements that have timed out.
			ProducerDb testDb = new Sqlite3ProducerDb(
					databaseFilePath.FullName);
			Producer producer = new Producer(prefix, suffix, face, keyChain, testDb);
			producer.createContentKey(testTime, new TestProducer.Anonymous_C1 (timeoutCount));
		}
	
		public void testProducerWithLink() {
			Name prefix = new Name("/prefix");
			Name suffix = new Name("/suffix");
			Name expectedInterest = new Name(prefix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_READ);
			expectedInterest.append(suffix);
			expectedInterest.append(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_E_KEY);
	
			double testTime = net.named_data.jndn.encrypt.Schedule.fromIsoString("20150101T100001");
	
			int[] timeoutCount = new int[] { 0 };
	
			// Prepare a LocalTestFace to instantly answer calls to expressInterest.
			
	
			TestProducer.LocalTestFace4  face = new TestProducer.LocalTestFace4 (expectedInterest, timeoutCount);
	
			// Verify that if no response is received, the producer appropriately times
			// out. The result vector should not contain elements that have timed out.
			Link link = new Link();
			link.addDelegation(10, new Name("/test1"));
			link.addDelegation(20, new Name("/test2"));
			link.addDelegation(100, new Name("/test3"));
			keyChain.sign(link);
			ProducerDb testDb = new Sqlite3ProducerDb(
					databaseFilePath.FullName);
			Producer producer = new Producer(prefix, suffix, face, keyChain,
					testDb, 3, link);
			producer.createContentKey(testTime, new TestProducer.Anonymous_C0 (timeoutCount));
		}
	
		internal FileInfo databaseFilePath;
	
		internal KeyChain keyChain;
		internal Name certificateName;
	
		internal IDictionary decryptionKeys; // key: Name, value: Blob
		internal IDictionary encryptionKeys; // key: Name, value: Data
		public sealed class Anonymous_C4 : Producer.OnEncryptedKeys {
			private readonly TestProducer.CheckEncryptionKeys  checkEncryptionKeys;
			private readonly double testTime1;
			private readonly net.named_data.jndn.Name.Component  testTimeRounded1;
	
			public Anonymous_C4(TestProducer.CheckEncryptionKeys  checkEncryptionKeys_0,
					double testTime1_1, net.named_data.jndn.Name.Component  testTimeRounded1_2) {
				this.checkEncryptionKeys = checkEncryptionKeys_0;
				this.testTime1 = testTime1_1;
				this.testTimeRounded1 = testTimeRounded1_2;
			}
	
			public void onEncryptedKeys(IList keys) {
				checkEncryptionKeys.checkEncryptionKeys(keys,
						testTime1, testTimeRounded1, 3);
			}
		}
		public sealed class Anonymous_C3 : Producer.OnEncryptedKeys {
			private readonly net.named_data.jndn.Name.Component  testTimeRounded2;
			private readonly TestProducer.CheckEncryptionKeys  checkEncryptionKeys;
			private readonly double testTime2;
	
			public Anonymous_C3(net.named_data.jndn.Name.Component  testTimeRounded2_0,
					TestProducer.CheckEncryptionKeys  checkEncryptionKeys_1, double testTime2_2) {
				this.testTimeRounded2 = testTimeRounded2_0;
				this.checkEncryptionKeys = checkEncryptionKeys_1;
				this.testTime2 = testTime2_2;
			}
	
			public void onEncryptedKeys(IList keys) {
				checkEncryptionKeys.checkEncryptionKeys(keys,
						testTime2, testTimeRounded2, 3);
			}
		}
		public sealed class Anonymous_C2 : Producer.OnEncryptedKeys {
			private readonly Name expectedInterest;
			private readonly int[] requestCount;
			private readonly Name cKeyName;
			private readonly Name timeMarkerThirdHop;
	
			public Anonymous_C2(Name expectedInterest_0, int[] requestCount_1,
					Name cKeyName_2, Name timeMarkerThirdHop_3) {
				this.expectedInterest = expectedInterest_0;
				this.requestCount = requestCount_1;
				this.cKeyName = cKeyName_2;
				this.timeMarkerThirdHop = timeMarkerThirdHop_3;
			}
	
			public void onEncryptedKeys(IList result) {
				Assert.AssertEquals(3, requestCount[0]);
				Assert.AssertEquals(1, result.Count);
	
				Data keyData = (Data) result[0];
				Name keyName = keyData.getName();
				Assert.AssertEquals(cKeyName, keyName.getSubName(0, 4));
				Assert.AssertEquals(timeMarkerThirdHop.get(0), keyName.get(4));
				Assert.AssertEquals(net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_FOR, keyName.get(5));
				Assert.AssertEquals(expectedInterest.append(timeMarkerThirdHop),
						keyName.getSubName(6));
			}
		}
		public sealed class Anonymous_C1 : Producer.OnEncryptedKeys {
			private readonly int[] timeoutCount;
	
			public Anonymous_C1(int[] timeoutCount_0) {
				this.timeoutCount = timeoutCount_0;
			}
	
			public void onEncryptedKeys(IList result) {
				Assert.AssertEquals(4, timeoutCount[0]);
				Assert.AssertEquals(0, result.Count);
			}
		}
		public sealed class Anonymous_C0 : Producer.OnEncryptedKeys {
			private readonly int[] timeoutCount;
	
			public Anonymous_C0(int[] timeoutCount_0) {
				this.timeoutCount = timeoutCount_0;
			}
	
			public void onEncryptedKeys(IList result) {
				Assert.AssertEquals(4, timeoutCount[0]);
				Assert.AssertEquals(0, result.Count);
			}
		}
		public class LocalTestFace : Face {
				private TestProducer outer_TestProducer;
				public LocalTestFace(TestProducer producer, Name timeMarker, int[] expressInterestCallCount) : base("localhost") {
					outer_TestProducer = producer;
		
					timeMarker_ = timeMarker;
					expressInterestCallCount_ = expressInterestCallCount;
				}
		
				public override long expressInterest(Interest interest, OnData onData,
						OnTimeout onTimeout, OnNetworkNack onNetworkNack,
						WireFormat wireFormat) {
					++expressInterestCallCount_[0];
		
					Name interestName = new Name(interest.getName());
					interestName.append(timeMarker_);
					Assert.AssertEquals(true, outer_TestProducer.encryptionKeys.Contains(interestName));
					onData.onData(interest, (Data) ILOG.J2CsMapping.Collections.Collections.Get(outer_TestProducer.encryptionKeys,interestName));
		
					return 0;
				}
		
				private readonly Name timeMarker_;
				private int[] expressInterestCallCount_;
			}
		public class CheckEncryptionKeys {
				private TestProducer outer_TestProducer;
				public CheckEncryptionKeys(TestProducer producer, int[] expressInterestCallCount,
						Blob[] contentKey, Name cKeyName_0, ProducerDb testDb) {
					outer_TestProducer = producer;
					expressInterestCallCount_ = expressInterestCallCount;
					contentKey_ = contentKey;
					cKeyName_ = cKeyName_0;
					testDb_ = testDb;
				}
		
				public void checkEncryptionKeys(IList result, double testTime,
						Name.Component roundedTime,
						int expectedExpressInterestCallCount) {
					Assert.AssertEquals(expectedExpressInterestCallCount,
							expressInterestCallCount_[0]);
		
					try {
						Assert.AssertEquals(true, testDb_.hasContentKey(testTime));
						contentKey_[0] = testDb_.getContentKey(testTime);
					} catch (ProducerDb.Error ex) {
						Assert.Fail("Error in ProducerDb: " + ex);
					}
		
					EncryptParams paras = new EncryptParams(
							net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep);
					for (int i = 0; i < result.Count; ++i) {
						Data key = (Data) result[i];
						Name keyName = key.getName();
						Assert.AssertEquals(cKeyName_, keyName.getSubName(0, 6));
						Assert.AssertEquals(keyName.get(6), roundedTime);
						Assert.AssertEquals(keyName.get(7), net.named_data.jndn.encrypt.algo.Encryptor.NAME_COMPONENT_FOR);
						Assert.AssertEquals(true,
								outer_TestProducer.decryptionKeys.Contains(keyName.getSubName(8)));
		
						Blob decryptionKey = (Blob) ILOG.J2CsMapping.Collections.Collections.Get(outer_TestProducer.decryptionKeys,keyName
													.getSubName(8));
						Assert.AssertEquals(true, decryptionKey.size() != 0);
						Blob encryptedKeyEncoding = key.getContent();
		
						EncryptedContent content = new EncryptedContent();
						try {
							content.wireDecode(encryptedKeyEncoding);
						} catch (EncodingException ex_0) {
							Assert.Fail("Error decoding EncryptedContent" + ex_0);
						}
						Blob encryptedKey = content.getPayload();
						Blob retrievedKey = null;
						try {
							retrievedKey = net.named_data.jndn.encrypt.algo.RsaAlgorithm.decrypt(decryptionKey,
									encryptedKey, paras);
						} catch (Exception ex_1) {
							Assert.Fail("Error in RsaAlgorithm.decrypt: " + ex_1);
						}
		
						Assert.AssertTrue(contentKey_[0].equals(retrievedKey));
					}
		
					Assert.AssertEquals(3, result.Count);
				}
		
				private readonly int[] expressInterestCallCount_;
				private readonly Blob[] contentKey_;
				private readonly Name cKeyName_;
				private readonly ProducerDb testDb_;
			}
		public class LocalTestFace2 : Face {
				private TestProducer outer_TestProducer;
				public LocalTestFace2(TestProducer producer, Name expectedInterest_0,
						Name timeMarkerFirstHop, Name timeMarkerSecondHop,
						Name timeMarkerThirdHop_1, int[] requestCount_2) : base("localhost") {
					outer_TestProducer = producer;
		
					expectedInterest_ = expectedInterest_0;
					timeMarkerFirstHop_ = timeMarkerFirstHop;
					timeMarkerSecondHop_ = timeMarkerSecondHop;
					timeMarkerThirdHop_ = timeMarkerThirdHop_1;
					requestCount_ = requestCount_2;
				}
		
				public override long expressInterest(Interest interest, OnData onData,
						OnTimeout onTimeout, OnNetworkNack onNetworkNack,
						WireFormat wireFormat) {
					Assert.AssertEquals(expectedInterest_, interest.getName());
		
					bool gotInterestName = false;
					Name interestName = null;
					for (int i = 0; i < 3; ++i) {
						interestName = new Name(interest.getName());
						if (i == 0)
							interestName.append(timeMarkerFirstHop_);
						else if (i == 1)
							interestName.append(timeMarkerSecondHop_);
						else if (i == 2)
							interestName.append(timeMarkerThirdHop_);
		
						// matchesName will check the Exclude.
						if (interest.matchesName(interestName)) {
							gotInterestName = true;
							++requestCount_[0];
							break;
						}
					}
		
					if (gotInterestName)
						onData.onData(interest,
								(Data) ILOG.J2CsMapping.Collections.Collections.Get(outer_TestProducer.encryptionKeys,interestName));
		
					return 0;
				}
		
				private readonly Name expectedInterest_;
				private readonly Name timeMarkerFirstHop_;
				private readonly Name timeMarkerSecondHop_;
				private readonly Name timeMarkerThirdHop_;
				private readonly int[] requestCount_;
			}
		internal class LocalTestFace3 : Face {
			public LocalTestFace3(Name expectedInterest_0, int[] timeoutCount_1) : base("localhost") {
				expectedInterest_ = expectedInterest_0;
				timeoutCount_ = timeoutCount_1;
			}
	
			public override long expressInterest(Interest interest, OnData onData,
					OnTimeout onTimeout, OnNetworkNack onNetworkNack,
					WireFormat wireFormat) {
				Assert.AssertEquals(expectedInterest_, interest.getName());
				++timeoutCount_[0];
				onTimeout.onTimeout(interest);
	
				return 0;
			}
	
			private readonly Name expectedInterest_;
			private readonly int[] timeoutCount_;
		}
		internal class LocalTestFace4 : Face {
			public LocalTestFace4(Name expectedInterest_0, int[] timeoutCount_1) : base("localhost") {
				expectedInterest_ = expectedInterest_0;
				timeoutCount_ = timeoutCount_1;
			}
	
			public override long expressInterest(Interest interest, OnData onData,
					OnTimeout onTimeout, OnNetworkNack onNetworkNack,
					WireFormat wireFormat) {
				Assert.AssertEquals(expectedInterest_, interest.getName());
				try {
					Assert.AssertEquals(3, interest.getLink().getDelegations().size());
				} catch (EncodingException ex) {
					Assert.Fail("Error in getLink: " + ex);
				}
				++timeoutCount_[0];
				onTimeout.onTimeout(interest);
	
				return 0;
			}
	
			private readonly Name expectedInterest_;
			private int[] timeoutCount_;
		}
	}
}
