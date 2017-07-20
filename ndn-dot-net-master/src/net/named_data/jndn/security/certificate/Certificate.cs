// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
//
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 /// <summary>
/// Copyright (C) 2014-2017 Regents of the University of California.
/// </summary>
///
namespace net.named_data.jndn.security.certificate {
	
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using net.named_data.jndn;
	using net.named_data.jndn.encoding;
	using net.named_data.jndn.encoding.der;
	using net.named_data.jndn.security;
	using net.named_data.jndn.util;
	
	public class Certificate : Data {
		/// <summary>
		/// The default constructor.
		/// </summary>
		///
		public Certificate() {
			this.subjectDescriptionList_ = new ArrayList();
			this.extensionList_ = new ArrayList();
			this.notBefore_ = System.Double.MaxValue;
			this.notAfter_ = -System.Double.MaxValue;
			this.key_ = new PublicKey();
		}
	
		/// <summary>
		/// Create a Certificate from the content in the data packet.
		/// </summary>
		///
		/// <param name="data">The data packet with the content to decode.</param>
		public Certificate(Data data) : base(data) {
			this.subjectDescriptionList_ = new ArrayList();
			this.extensionList_ = new ArrayList();
			this.notBefore_ = System.Double.MaxValue;
			this.notAfter_ = -System.Double.MaxValue;
			this.key_ = new PublicKey();
			decode();
		}
	
		/// <summary>
		/// Encode the contents of the certificate in DER format and set the Content
		/// and MetaInfo fields.
		/// </summary>
		///
		public void encode() {
			DerNode root = toDer();
			setContent(root.encode());
			getMetaInfo().setType(net.named_data.jndn.ContentType.KEY);
		}
	
		/// <summary>
		/// Override to call the base class wireDecode then populate the certificate
		/// fields.
		/// </summary>
		///
		/// <param name="input">The input byte array to be decoded as an immutable Blob.</param>
		/// <param name="wireFormat">A WireFormat object used to decode the input.</param>
		public override void wireDecode(Blob input, WireFormat wireFormat) {
			base.wireDecode(input,wireFormat);
			try {
				decode();
			} catch (DerDecodingException ex) {
				throw new EncodingException(ex.Message);
			}
		}
	
		/// <summary>
		/// Add a subject description.
		/// </summary>
		///
		/// <param name="description">The description to be added.</param>
		public void addSubjectDescription(
				CertificateSubjectDescription description) {
			ILOG.J2CsMapping.Collections.Collections.Add(subjectDescriptionList_,description);
		}
	
		// List of CertificateSubjectDescription.
		public IList getSubjectDescriptionList() {
			return subjectDescriptionList_;
		}
	
		/// <summary>
		/// Add a certificate extension.
		/// </summary>
		///
		/// <param name="extension">the extension to be added</param>
		public void addExtension(CertificateExtension extension) {
			ILOG.J2CsMapping.Collections.Collections.Add(extensionList_,extension);
		}
	
		// List of CertificateExtension.
		public IList getExtensionList() {
			return extensionList_;
		}
	
		public void setNotBefore(double notBefore) {
			notBefore_ = notBefore;
		}
	
		public double getNotBefore() {
			return notBefore_;
		}
	
		public void setNotAfter(double notAfter) {
			notAfter_ = notAfter;
		}
	
		public double getNotAfter() {
			return notAfter_;
		}
	
		public void setPublicKeyInfo(PublicKey key) {
			key_ = key;
		}
	
		public PublicKey getPublicKeyInfo() {
			return key_;
		}
	
		/// <summary>
		/// Get the public key DER encoding.
		/// </summary>
		///
		/// <returns>The DER encoding Blob.</returns>
		/// <exception cref="System.Exception">if the public key is not set.</exception>
		public Blob getPublicKeyDer() {
			if (key_.getKeyDer().isNull())
				throw new Exception("The public key is not set");
	
			return key_.getKeyDer();
		}
	
		/// <summary>
		/// Check if the certificate is valid.
		/// </summary>
		///
		/// <returns>True if the current time is earlier than notBefore.</returns>
		public bool isTooEarly() {
			double now = net.named_data.jndn.util.Common.getNowMilliseconds();
			return now < getNotBefore();
		}
	
		/// <summary>
		/// Check if the certificate is valid.
		/// </summary>
		///
		/// <returns>True if the current time is later than notAfter.</returns>
		public bool isTooLate() {
			double now = net.named_data.jndn.util.Common.getNowMilliseconds();
			return now > getNotAfter();
		}
	
		public bool isInValidityPeriod(double time) {
			// Debug: Generalize this from Sha256WithRsaSignature.
			return ((Sha256WithRsaSignature) getSignature()).getValidityPeriod()
					.isValid(time);
		}
	
		/// <summary>
		/// Encode the certificate fields in DER format.
		/// </summary>
		///
		/// <returns>The DER encoded contents of the certificate.</returns>
		private net.named_data.jndn.encoding.der.DerNode.DerSequence  toDer() {
			net.named_data.jndn.encoding.der.DerNode.DerSequence  root = new net.named_data.jndn.encoding.der.DerNode.DerSequence ();
			net.named_data.jndn.encoding.der.DerNode.DerSequence  validity = new net.named_data.jndn.encoding.der.DerNode.DerSequence ();
			net.named_data.jndn.encoding.der.DerNode.DerGeneralizedTime  notBefore = new net.named_data.jndn.encoding.der.DerNode.DerGeneralizedTime (getNotBefore());
			net.named_data.jndn.encoding.der.DerNode.DerGeneralizedTime  notAfter = new net.named_data.jndn.encoding.der.DerNode.DerGeneralizedTime (getNotAfter());
	
			validity.addChild(notBefore);
			validity.addChild(notAfter);
	
			root.addChild(validity);
	
			net.named_data.jndn.encoding.der.DerNode.DerSequence  subjectList = new net.named_data.jndn.encoding.der.DerNode.DerSequence ();
			for (int i = 0; i < subjectDescriptionList_.Count; ++i)
				subjectList
						.addChild(((CertificateSubjectDescription) subjectDescriptionList_[i]).toDer());
	
			root.addChild(subjectList);
			root.addChild(key_.toDer());
	
			if (extensionList_.Count > 0) {
				net.named_data.jndn.encoding.der.DerNode.DerSequence  extensionList = new net.named_data.jndn.encoding.der.DerNode.DerSequence ();
				for (int i_0 = 0; i_0 < extensionList_.Count; ++i_0)
					extensionList.addChild(((CertificateExtension) extensionList_[i_0]).toDer());
				root.addChild(extensionList);
			}
	
			return root;
		}
	
		/// <summary>
		/// Populate the fields by the decoding DER data from the Content.
		/// </summary>
		///
		private void decode() {
			DerNode parsedNode = net.named_data.jndn.encoding.der.DerNode.parse(getContent().buf());
	
			// We need to ensure that there are:
			//   validity (notBefore, notAfter)
			//   subject list
			//   public key
			//   (optional) extension list
	
			IList rootChildren = parsedNode.getChildren();
			// 1st: validity info
			IList validityChildren = net.named_data.jndn.encoding.der.DerNode.getSequence(rootChildren, 0)
					.getChildren();
			notBefore_ = ((Double)((net.named_data.jndn.encoding.der.DerNode.DerGeneralizedTime )validityChildren[0]).toVal());
			notAfter_ = ((Double)((net.named_data.jndn.encoding.der.DerNode.DerGeneralizedTime )validityChildren[1]).toVal());
	
			// 2nd: subjectList
			IList subjectChildren = net.named_data.jndn.encoding.der.DerNode.getSequence(rootChildren, 1)
					.getChildren();
			for (int i = 0; i < subjectChildren.Count; ++i) {
				net.named_data.jndn.encoding.der.DerNode.DerSequence  sd = net.named_data.jndn.encoding.der.DerNode.getSequence(subjectChildren, i);
				IList descriptionChildren = sd.getChildren();
				String oidStr = (String) ((DerNode) descriptionChildren[0])
						.toVal();
				String value_ren = ""
						+ ((Blob) ((DerNode) descriptionChildren[1]).toVal());
	
				addSubjectDescription(new CertificateSubjectDescription(oidStr,
						value_ren));
			}
	
			// 3rd: public key
			Blob publicKeyInfo = ((DerNode) rootChildren[2]).encode();
			try {
				key_ = new PublicKey(publicKeyInfo);
			} catch (UnrecognizedKeyFormatException ex) {
				throw new DerDecodingException(ex.Message);
			}
	
			if (rootChildren.Count > 3) {
				IList extensionChildren = net.named_data.jndn.encoding.der.DerNode.getSequence(rootChildren, 3)
						.getChildren();
				for (int i_0 = 0; i_0 < extensionChildren.Count; ++i_0) {
					net.named_data.jndn.encoding.der.DerNode.DerSequence  extInfo = net.named_data.jndn.encoding.der.DerNode.getSequence(extensionChildren, i_0);
	
					IList children = extInfo.getChildren();
					String oidStr_1 = (String) ((DerNode) children[0]).toVal();
					bool isCritical = (bool)(((Boolean)((net.named_data.jndn.encoding.der.DerNode.DerBoolean )children[1]).toVal()));
					Blob value_2 = (Blob) ((DerNode) children[2]).toVal();
					addExtension(new CertificateExtension(oidStr_1, isCritical, value_2));
				}
			}
		}
	
		public override String ToString() {
			String s = "Certificate name:\n";
			s += "  " + getName().toUri() + "\n";
			s += "Validity:\n";
	
			SimpleDateFormat dateFormat = new SimpleDateFormat("yyyyMMdd'T'HHmmss");
			dateFormat.setTimeZone(System.Collections.TimeZone.getTimeZone("UTC"));
			String notBeforeStr = dateFormat
					.format(net.named_data.jndn.util.Common.millisecondsSince1970ToDate((long) Math.Round(getNotBefore(),MidpointRounding.AwayFromZero)));
			String notAfterStr = dateFormat.format(net.named_data.jndn.util.Common
					.millisecondsSince1970ToDate((long) Math.Round(getNotAfter(),MidpointRounding.AwayFromZero)));
	
			s += "  NotBefore: " + notBeforeStr + "\n";
			s += "  NotAfter: " + notAfterStr + "\n";
			for (int i = 0; i < subjectDescriptionList_.Count; ++i) {
				CertificateSubjectDescription sd = (CertificateSubjectDescription) subjectDescriptionList_[i];
				s += "Subject Description:\n";
				s += "  " + sd.getOidString() + ": " + sd.getValue() + "\n";
			}
	
			s += "Public key bits:\n";
			Blob keyDer = getPublicKeyDer();
			String encodedKey = net.named_data.jndn.util.Common.base64Encode(keyDer.getImmutableArray());
			for (int i_0 = 0; i_0 < encodedKey.Length; i_0 += 64)
				s += encodedKey.Substring(i_0,(Math.Min(i_0 + 64,encodedKey.Length))-(i_0))
						+ "\n";
	
			if (extensionList_.Count > 0) {
				s += "Extensions:\n";
				for (int i_1 = 0; i_1 < extensionList_.Count; ++i_1) {
					CertificateExtension ext = (CertificateExtension) extensionList_[i_1];
					s += "  OID: " + ext.getOid() + "\n";
					s += "  Is critical: " + ((ext.getIsCritical()) ? 'Y' : 'N')
							+ "\n";
	
					s += "  Value: " + ext.getValue().toHex() + "\n";
				}
			}
	
			return s;
		}
	
		// Use ArrayList without generics so it works with older Java compilers.
		private readonly ArrayList subjectDescriptionList_; // of CertificateSubjectDescription
		private readonly ArrayList extensionList_; // of CertificateExtension
		private double notBefore_; // MillisecondsSince1970
		private double notAfter_; // MillisecondsSince1970
		private PublicKey key_;
	}
}
