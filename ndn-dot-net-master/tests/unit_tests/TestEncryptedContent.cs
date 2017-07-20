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
namespace net.named_data.jndn.tests.unit_tests {
	
	using ILOG.J2CsMapping.NIO;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using net.named_data.jndn;
	using net.named_data.jndn.encoding;
	using net.named_data.jndn.encrypt;
	using net.named_data.jndn.util;
	
	public class TestEncryptedContent {
		// Convert the int array to a ByteBuffer.
		private static ByteBuffer toBuffer(int[] array) {
			ByteBuffer result = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(array.Length);
			for (int i = 0; i < array.Length; ++i)
				result.put((byte) (array[i] & 0xff));
	
			result.flip();
			return result;
		}
	
		private static readonly ByteBuffer encrypted = toBuffer(new int[] {
				0x82,
				0x30, // EncryptedContent
				0x1c,
				0x16, // KeyLocator
				0x07,
				0x14, // Name
				0x08, 0x04, 0x74, 0x65, 0x73,
				0x74, // 'test'
				0x08, 0x03, 0x6b, 0x65,
				0x79, // 'key'
				0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f,
				0x72, // 'locator'
				0x83,
				0x01, // EncryptedAlgorithm
				0x03, 0x85,
				0x0a, // InitialVector
				0x72, 0x61, 0x6e, 0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73, 0x84,
				0x07, // EncryptedPayload
				0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 });
	
		private static readonly ByteBuffer encryptedNoIv = toBuffer(new int[] { 0x82,
				0x24, // EncryptedContent
				0x1c, 0x16, // KeyLocator
				0x07, 0x14, // Name
				0x08, 0x04, 0x74, 0x65, 0x73, 0x74, // 'test'
				0x08, 0x03, 0x6b, 0x65, 0x79, // 'key'
				0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f, 0x72, // 'locator'
				0x83, 0x01, // EncryptedAlgorithm
				0x03, 0x84, 0x07, // EncryptedPayload
				0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 });
	
		private static readonly ByteBuffer message = toBuffer(new int[] { 0x63, 0x6f,
				0x6e, 0x74, 0x65, 0x6e, 0x74 });
	
		private static readonly ByteBuffer iv = toBuffer(new int[] { 0x72, 0x61, 0x6e,
				0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73 });
	
		public void testConstructor() {
			// Check default settings.
			EncryptedContent content = new EncryptedContent();
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.NONE, content.getAlgorithmType());
			Assert.AssertEquals(true, content.getPayload().isNull());
			Assert.AssertEquals(true, content.getInitialVector().isNull());
			Assert.AssertEquals(net.named_data.jndn.KeyLocatorType.NONE, content.getKeyLocator().getType());
	
			// Check an encrypted content with IV.
			KeyLocator keyLocator = new KeyLocator();
			keyLocator.setType(net.named_data.jndn.KeyLocatorType.KEYNAME);
			keyLocator.getKeyName().set("/test/key/locator");
			EncryptedContent rsaOaepContent = new EncryptedContent();
			rsaOaepContent.setAlgorithmType(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep)
					.setKeyLocator(keyLocator).setPayload(new Blob(message, false))
					.setInitialVector(new Blob(iv, false));
	
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep,
					rsaOaepContent.getAlgorithmType());
			Assert.AssertTrue(rsaOaepContent.getPayload().equals(new Blob(message, false)));
			Assert.AssertTrue(rsaOaepContent.getInitialVector()
					.equals(new Blob(iv, false)));
			Assert.AssertTrue(rsaOaepContent.getKeyLocator().getType() != net.named_data.jndn.KeyLocatorType.NONE);
			Assert.AssertTrue(rsaOaepContent.getKeyLocator().getKeyName()
					.equals(new Name("/test/key/locator")));
	
			// Encoding.
			Blob encryptedBlob = new Blob(encrypted, false);
			Blob encoded = rsaOaepContent.wireEncode();
	
			Assert.AssertTrue(encryptedBlob.equals(encoded));
	
			// Decoding.
			EncryptedContent rsaOaepContent2 = new EncryptedContent();
			rsaOaepContent2.wireDecode(encryptedBlob);
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep,
					rsaOaepContent2.getAlgorithmType());
			Assert.AssertTrue(rsaOaepContent2.getPayload()
					.equals(new Blob(message, false)));
			Assert.AssertTrue(rsaOaepContent2.getInitialVector().equals(
					new Blob(iv, false)));
			Assert.AssertTrue(rsaOaepContent2.getKeyLocator().getType() != net.named_data.jndn.KeyLocatorType.NONE);
			Assert.AssertTrue(rsaOaepContent2.getKeyLocator().getKeyName()
					.equals(new Name("/test/key/locator")));
	
			// Check the no IV case.
			EncryptedContent rsaOaepContentNoIv = new EncryptedContent();
			rsaOaepContentNoIv.setAlgorithmType(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep)
					.setKeyLocator(keyLocator).setPayload(new Blob(message, false));
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep,
					rsaOaepContentNoIv.getAlgorithmType());
			Assert.AssertTrue(rsaOaepContentNoIv.getPayload().equals(
					new Blob(message, false)));
			Assert.AssertTrue(rsaOaepContentNoIv.getInitialVector().isNull());
			Assert.AssertTrue(rsaOaepContentNoIv.getKeyLocator().getType() != net.named_data.jndn.KeyLocatorType.NONE);
			Assert.AssertTrue(rsaOaepContentNoIv.getKeyLocator().getKeyName()
					.equals(new Name("/test/key/locator")));
	
			// Encoding.
			Blob encryptedBlob2 = new Blob(encryptedNoIv, false);
			Blob encodedNoIV = rsaOaepContentNoIv.wireEncode();
			Assert.AssertTrue(encryptedBlob2.equals(encodedNoIV));
	
			// Decoding.
			EncryptedContent rsaOaepContentNoIv2 = new EncryptedContent();
			rsaOaepContentNoIv2.wireDecode(encryptedBlob2);
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep,
					rsaOaepContentNoIv2.getAlgorithmType());
			Assert.AssertTrue(rsaOaepContentNoIv2.getPayload().equals(
					new Blob(message, false)));
			Assert.AssertTrue(rsaOaepContentNoIv2.getInitialVector().isNull());
			Assert.AssertTrue(rsaOaepContentNoIv2.getKeyLocator().getType() != net.named_data.jndn.KeyLocatorType.NONE);
			Assert.AssertTrue(rsaOaepContentNoIv2.getKeyLocator().getKeyName()
					.equals(new Name("/test/key/locator")));
		}
	
		public void testDecodingError() {
			EncryptedContent encryptedContent = new EncryptedContent();
	
			Blob errorBlob1 = new Blob(toBuffer(new int[] {
					0x1f,
					0x30, // Wrong EncryptedContent (0x82, 0x24)
					0x1c,
					0x16, // KeyLocator
					0x07,
					0x14, // Name
					0x08, 0x04, 0x74, 0x65, 0x73, 0x74, 0x08, 0x03, 0x6b, 0x65,
					0x79, 0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f, 0x72,
					0x83,
					0x01, // EncryptedAlgorithm
					0x00, 0x85,
					0x0a, // InitialVector
					0x72, 0x61, 0x6e, 0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73,
					0x84, 0x07, // EncryptedPayload
					0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 }), false);
			try {
				encryptedContent.wireDecode(errorBlob1);
				Assert.Fail("wireDecode did not throw an exception");
			} catch (EncodingException ex) {
			} catch (Exception ex_0) {
				Assert.Fail("wireDecode did not throw EncodingException");
			}
	
			Blob errorBlob2 = new Blob(toBuffer(new int[] {
					0x82,
					0x30, // EncryptedContent
					0x1d,
					0x16, // Wrong KeyLocator (0x1c, 0x16)
					0x07,
					0x14, // Name
					0x08, 0x04, 0x74, 0x65, 0x73, 0x74, 0x08, 0x03, 0x6b, 0x65,
					0x79, 0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f, 0x72,
					0x83,
					0x01, // EncryptedAlgorithm
					0x00, 0x85,
					0x0a, // InitialVector
					0x72, 0x61, 0x6e, 0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73,
					0x84, 0x07, // EncryptedPayload
					0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 }), false);
			try {
				encryptedContent.wireDecode(errorBlob2);
				Assert.Fail("wireDecode did not throw an exception");
			} catch (EncodingException ex_1) {
			} catch (Exception ex_2) {
				Assert.Fail("wireDecode did not throw EncodingException");
			}
	
			Blob errorBlob3 = new Blob(toBuffer(new int[] {
					0x82,
					0x30, // EncryptedContent
					0x1c,
					0x16, // KeyLocator
					0x07,
					0x14, // Name
					0x08, 0x04, 0x74, 0x65, 0x73, 0x74, 0x08, 0x03, 0x6b, 0x65,
					0x79, 0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f, 0x72,
					0x1d,
					0x01, // Wrong EncryptedAlgorithm (0x83, 0x01)
					0x00, 0x85,
					0x0a, // InitialVector
					0x72, 0x61, 0x6e, 0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73,
					0x84, 0x07, // EncryptedPayload
					0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 }), false);
			try {
				encryptedContent.wireDecode(errorBlob3);
				Assert.Fail("wireDecode did not throw an exception");
			} catch (EncodingException ex_3) {
			} catch (Exception ex_4) {
				Assert.Fail("wireDecode did not throw EncodingException");
			}
	
			Blob errorBlob4 = new Blob(toBuffer(new int[] { 0x82,
					0x30, // EncryptedContent
					0x1c,
					0x16, // KeyLocator
					0x07,
					0x14, // Name
					0x08, 0x04, 0x74, 0x65, 0x73,
					0x74, // 'test'
					0x08, 0x03, 0x6b, 0x65,
					0x79, // 'key'
					0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f,
					0x72, // 'locator'
					0x83,
					0x01, // EncryptedAlgorithm
					0x00, 0x1f,
					0x0a, // InitialVector (0x84, 0x0a)
					0x72, 0x61, 0x6e, 0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73,
					0x84, 0x07, // EncryptedPayload
					0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 }), false);
			try {
				encryptedContent.wireDecode(errorBlob4);
				Assert.Fail("wireDecode did not throw an exception");
			} catch (EncodingException ex_5) {
			} catch (Exception ex_6) {
				Assert.Fail("wireDecode did not throw EncodingException");
			}
	
			Blob errorBlob5 = new Blob(toBuffer(new int[] { 0x82,
					0x30, // EncryptedContent
					0x1c,
					0x16, // KeyLocator
					0x07,
					0x14, // Name
					0x08, 0x04, 0x74, 0x65, 0x73,
					0x74, // 'test'
					0x08, 0x03, 0x6b, 0x65,
					0x79, // 'key'
					0x08, 0x07, 0x6c, 0x6f, 0x63, 0x61, 0x74, 0x6f,
					0x72, // 'locator'
					0x83,
					0x01, // EncryptedAlgorithm
					0x00, 0x85,
					0x0a, // InitialVector
					0x72, 0x61, 0x6e, 0x64, 0x6f, 0x6d, 0x62, 0x69, 0x74, 0x73,
					0x21, 0x07, // EncryptedPayload (0x85, 0x07)
					0x63, 0x6f, 0x6e, 0x74, 0x65, 0x6e, 0x74 }), false);
			try {
				encryptedContent.wireDecode(errorBlob5);
				Assert.Fail("wireDecode did not throw an exception");
			} catch (EncodingException ex_7) {
			} catch (Exception ex_8) {
				Assert.Fail("wireDecode did not throw EncodingException");
			}
	
			Blob errorBlob6 = new Blob(toBuffer(new int[] { 0x82, 0x00 // Empty EncryptedContent
					}), false);
			try {
				encryptedContent.wireDecode(errorBlob6);
				Assert.Fail("wireDecode did not throw an exception");
			} catch (EncodingException ex_9) {
			} catch (Exception ex_10) {
				Assert.Fail("wireDecode did not throw EncodingException");
			}
		}
	
		public void testSetterGetter() {
			EncryptedContent content = new EncryptedContent();
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.NONE, content.getAlgorithmType());
			Assert.AssertEquals(true, content.getPayload().isNull());
			Assert.AssertEquals(true, content.getInitialVector().isNull());
			Assert.AssertEquals(net.named_data.jndn.KeyLocatorType.NONE, content.getKeyLocator().getType());
	
			content.setAlgorithmType(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep);
			Assert.AssertEquals(net.named_data.jndn.encrypt.algo.EncryptAlgorithmType.RsaOaep, content.getAlgorithmType());
			Assert.AssertEquals(true, content.getPayload().isNull());
			Assert.AssertEquals(true, content.getInitialVector().isNull());
			Assert.AssertEquals(net.named_data.jndn.KeyLocatorType.NONE, content.getKeyLocator().getType());
	
			KeyLocator keyLocator = new KeyLocator();
			keyLocator.setType(net.named_data.jndn.KeyLocatorType.KEYNAME);
			keyLocator.getKeyName().set("/test/key/locator");
			content.setKeyLocator(keyLocator);
			Assert.AssertTrue(content.getKeyLocator().getType() != net.named_data.jndn.KeyLocatorType.NONE);
			Assert.AssertTrue(content.getKeyLocator().getKeyName()
					.equals(new Name("/test/key/locator")));
			Assert.AssertEquals(true, content.getPayload().isNull());
			Assert.AssertEquals(true, content.getInitialVector().isNull());
	
			content.setPayload(new Blob(message, false));
			Assert.AssertTrue(content.getPayload().equals(new Blob(message, false)));
	
			content.setInitialVector(new Blob(iv, false));
			Assert.AssertTrue(content.getInitialVector().equals(new Blob(iv, false)));
	
			Blob encoded = content.wireEncode();
			Blob contentBlob = new Blob(encrypted, false);
			Assert.AssertTrue(contentBlob.equals(encoded));
		}
	}}
