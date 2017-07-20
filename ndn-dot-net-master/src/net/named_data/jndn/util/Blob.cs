// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
//
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
 /// <summary>
/// Copyright (C) 2013-2017 Regents of the University of California.
/// </summary>
///
namespace net.named_data.jndn.util {
	
	using ILOG.J2CsMapping.NIO;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.CompilerServices;
	using System.Text;
	
	/// <summary>
	/// A Blob holds a pointer to an immutable ByteBuffer.  We use an immutable
	/// buffer so that it is OK to pass the object into methods because the new or
	/// old owner can’t change the bytes.
	/// Note that  the pointer to the ByteBuffer can be null.
	/// </summary>
	///
	public class Blob : IComparable {
		/// <summary>
		/// Create a new Blob with a null pointer.
		/// </summary>
		///
		public Blob() {
			this.haveHashCode_ = false;
			buffer_ = null;
		}
	
		/// <summary>
		/// Create a new Blob and take another pointer to the given blob's buffer.
		/// </summary>
		///
		/// <param name="blob">The Blob from which we take another pointer to the same buffer.</param>
		public Blob(Blob blob) {
			this.haveHashCode_ = false;
			if (blob != null)
				buffer_ = blob.buffer_;
			else
				buffer_ = null;
		}
	
		/// <summary>
		/// Create a new Blob from an existing ByteBuffer.  IMPORTANT: If copy is
		/// false, after calling this constructor, if you keep a pointer to the buffer
		/// then you must treat it as immutable and promise not to change it.
		/// </summary>
		///
		/// <param name="buffer"></param>
		/// <param name="copy"></param>
		public Blob(ByteBuffer buffer, bool copy) {
			this.haveHashCode_ = false;
			if (buffer != null) {
				if (copy) {
					buffer_ = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(buffer.remaining());
	
					// Put updates buffer.position(), so save and restore it.
					int savePosition = buffer.position();
					buffer_.put(buffer);
					buffer.position(savePosition);
	
					buffer_.flip();
				} else
					buffer_ = buffer.slice();
			} else
				buffer_ = null;
		}
	
		/// <summary>
		/// Create a new Blob from the the byte array. IMPORTANT: If copy is false,
		/// after calling this constructor, if you keep a pointer to the buffer then
		/// you must treat it as immutable and promise not to change it.
		/// </summary>
		///
		/// <param name="value">The byte array. If copy is true, this makes a copy.</param>
		/// <param name="copy"></param>
		public Blob(byte[] value_ren, bool copy) {
			this.haveHashCode_ = false;
			if (copy) {
				buffer_ = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(value_ren.Length);
				buffer_.put(value_ren);
				buffer_.flip();
			} else
				buffer_ = ILOG.J2CsMapping.NIO.ByteBuffer.wrap(value_ren);
		}
	
		/// <summary>
		/// Create a new Blob with a copy of the bytes in the array.
		/// </summary>
		///
		/// <param name="value">The byte array to copy.</param>
		public Blob(byte[] value_ren) {
			this.haveHashCode_ = false;
			buffer_ = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(value_ren.Length);
			buffer_.put(value_ren);
			buffer_.flip();
		}
	
		/// <summary>
		/// Create a new Blob with a copy of the bytes in the array.
		/// </summary>
		///
		/// <param name="value"></param>
		public Blob(int[] value_ren) {
			this.haveHashCode_ = false;
			buffer_ = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(value_ren.Length);
			for (int i = 0; i < value_ren.Length; ++i)
				buffer_.put((byte) value_ren[i]);
			buffer_.flip();
		}
	
		/// <summary>
		/// Create a new Blob from the UTF-8 encoding of the Unicode string.
		/// </summary>
		///
		/// <param name="value">The Unicode string which is encoded as UTF-8.</param>
		public Blob(String value_ren) {
			this.haveHashCode_ = false;
			byte[] utf8;
			try {
				utf8 = ILOG.J2CsMapping.Util.StringUtil.GetBytes(value_ren,"UTF-8");
			} catch (IOException ex) {
				// We don't expect this to happen.
				throw new Exception("UTF-8 encoder not supported: " + ex.Message);
			}
			buffer_ = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(utf8.Length);
			buffer_.put(utf8);
			buffer_.flip();
		}
	
		/// <summary>
		/// Get the read-only ByteBuffer.
		/// </summary>
		///
		/// <returns>The read-only ByteBuffer using asReadOnlyBuffer(), or null if the
		/// pointer is null.</returns>
		public ByteBuffer buf() {
			if (buffer_ != null)
				// We call asReadOnlyBuffer each time because it is still allowed to
				//   change the position and limit on a read-only buffer, and we don't
				//   want the caller to modify our buffer_.
				return buffer_.asReadOnlyBuffer();
			else
				return null;
		}
	
		/// <summary>
		/// Get a byte array by calling ByteBuffer.array() if possible (because the
		/// ByteBuffer slice covers the entire backing array). Otherwise return byte
		/// array as a copy of the ByteBuffer. This is called immutableArray to remind
		/// you not to change the contents of the returned array. This method is
		/// necessary because the read-only ByteBuffer returned by buf() doesn't allow
		/// you to call array().
		/// </summary>
		///
		/// <returns>A byte array which you should not modify, or null if the pointer
		/// is null.</returns>
		public byte[] getImmutableArray() {
			if (buffer_ != null) {
				// We can't call array() on a read only ByteBuffer.
				if (!buffer_.isReadOnly()) {
					byte[] array = buffer_.array();
					if (array.Length == buffer_.remaining())
						// Assume the buffer_ covers the entire backing array, so just return.
						return array;
				}
	
				// We have to copy to a new byte array.
				ByteBuffer tempBuffer = ILOG.J2CsMapping.NIO.ByteBuffer.allocate(buffer_.remaining());
				int savePosition = buffer_.position();
				tempBuffer.put(buffer_);
				buffer_.position(savePosition);
				tempBuffer.flip();
				return tempBuffer.array();
			} else
				return null;
		}
	
		/// <summary>
		/// Get the size of the buffer.
		/// </summary>
		///
		/// <returns>The length (remaining) of the ByteBuffer, or 0 if the pointer is
		/// null.</returns>
		public int size() {
			if (buffer_ != null)
				return buffer_.remaining();
			else
				return 0;
		}
	
		/// <summary>
		/// Check if the buffer pointer is null.
		/// </summary>
		///
		/// <returns>True if the buffer pointer is null, otherwise false.</returns>
		public bool isNull() {
			return buffer_ == null;
		}
	
		/// <summary>
		/// Return a hex string of buf() from position to limit.
		/// </summary>
		///
		/// <returns>A string of hex bytes, or "" if the buffer is null.</returns>
		public String toHex() {
			if (buffer_ == null)
				return "";
			else
				return toHex(buffer_);
		}
	
		/// <summary>
		/// Write a hex string of the contents of buffer from position to limit to the
		/// output.
		/// </summary>
		///
		/// <param name="buffer">The buffer.</param>
		/// <param name="output">The StringBuffer to write to.</param>
		public static void toHex(ByteBuffer buffer, StringBuilder output) {
			for (int i = buffer.position(); i < buffer.limit(); ++i) {
				String hex = ILOG.J2CsMapping.Util.IlNumber.ToString((int) buffer.get(i) & 0xff,16);
				if (hex.Length <= 1)
					// Append the leading zero.
					output.append("0");
				output.append(hex);
			}
		}
	
		/// <summary>
		/// Return a hex string of the contents of buffer from position to limit.
		/// </summary>
		///
		/// <param name="buffer">The buffer.</param>
		/// <returns>A string of hex bytes.</returns>
		public static String toHex(ByteBuffer buffer) {
			StringBuilder output = new StringBuilder(buffer.remaining() * 2);
			toHex(buffer, output);
			return output.toString();
		}
	
		public bool equals(Blob other) {
			if (buffer_ == null)
				return other.buffer_ == null;
			else if (other.isNull())
				return false;
			else
				return buffer_.equals(other.buffer_);
		}
	
		public override bool Equals(Object other) {
			if (!(other  is  Blob))
				return false;
	
			return equals((Blob) other);
		}
	
		/// <summary>
		/// Compare this to the other Blob using byte-by-byte comparison from their
		/// position to their limit. If this and other are both isNull(), then this
		/// returns 0. If this isNull() and the other is not, return -1. If this is not
		/// isNull() and the other is, return 1. We compare explicitly because a Blob
		/// uses a ByteBuffer which compares based on signed byte, not unsigned byte.
		/// </summary>
		///
		/// <param name="other">The other Blob to compare with.</param>
		/// <returns>0 If they compare equal, -1 if self is less than other, or 1 if
		/// self is greater than other.  If both are equal up to the shortest, then
		/// return -1 if self is shorter than other, or 1 of self is longer than other.</returns>
		public int compare(Blob other) {
			if (buffer_ == null && other.buffer_ == null)
				return 0;
			if (buffer_ == null && other.buffer_ != null)
				return -1;
			if (buffer_ != null && other.buffer_ == null)
				return 1;
	
			// Manually compare elements as unsigned.
			int r = Math.Min(buffer_.remaining(),other.buffer_.remaining());
			for (int i = 0; i < r; ++i) {
				// b & 0xff makes the byte unsigned and returns an int.
				int xThis = buffer_.get(buffer_.position() + i) & 0xff;
				int xOther = other.buffer_.get(other.buffer_.position() + i) & 0xff;
	
				if (xThis < xOther)
					return -1;
				if (xThis > xOther)
					return 1;
			}
	
			// They are equal up to the shorter.
			if (buffer_.remaining() < other.buffer_.remaining())
				return -1;
			if (buffer_.remaining() > other.buffer_.remaining())
				return 1;
			return 0;
		}
	
		public int compareTo(Object o) {
			return this.compare((Blob) o);
		}
	
		// Also include this version for portability.
		public int CompareTo(Object o) {
			return this.compare((Blob) o);
		}
	
		/// <summary>
		/// If the hash code is already computed then return it, otherwise compute and
		/// return the hash code.
		/// </summary>
		///
		/// <returns>The hash code for the buffer, or 0 if the buffer is null.</returns>
		public override int GetHashCode() {
			if (!haveHashCode_) {
				if (buffer_ == null)
					hashCode_ = 0;
				else
					hashCode_ = buffer_.GetHashCode();
	
				haveHashCode_ = true;
			}
	
			return hashCode_;
		}
	
		/// <summary>
		/// Decode the byte array as UTF8 and return the Unicode string.
		/// </summary>
		///
		/// <returns>A unicode string, or "" if the buffer is null.</returns>
		public override String ToString() {
			if (buffer_ == null)
				return "";
			else {
				try {
					return ILOG.J2CsMapping.Util.StringUtil.NewString(getImmutableArray(),"UTF-8");
				} catch (IOException ex) {
					// We don't expect this to happen.
					throw new Exception("UTF-8 decoder not supported: "
							+ ex.Message);
				}
			}
		}
	
		private readonly ByteBuffer buffer_;
		private bool haveHashCode_;
		private int hashCode_;
	}
}
