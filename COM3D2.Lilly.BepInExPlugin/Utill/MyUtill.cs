﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class MyUtill
    {
		/// <summary>
		/// 닷넷 3.5에선 컴파일 안되서 직접 복사 수정
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="separator"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static string Join<T>(string separator, IEnumerable<T> values)
		{
			if (values == null)
			{
				return "";
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string result;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text = t.ToString();
						if (text != null)
						{
							stringBuilder.Append(text);
						}
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							T t = enumerator.Current;
							string text2 = t.ToString();
							if (text2 != null)
							{
								stringBuilder.Append(text2);
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

        internal static string GetMaidFullNale(Maid maid)
        {
			StringBuilder s = new StringBuilder();
			if (maid.status != null)
			{
				s.Append(maid.status.firstName);
				s.Append(" , " + maid.status.lastName);
				if (maid.status.personal != null)
				{
					s.Append(" , " + maid.status.personal.id);
					s.Append(" , " + maid.status.personal.replaceText);
					s.Append(" , " + maid.status.personal.uniqueName);
					s.Append(" , " + maid.status.personal.drawName);
				}

			}
			return s.ToString();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodBase"></param>
        /// <returns></returns>
        public static string GetClassMethodName(System.Reflection.MethodBase methodBase)
        {
			return methodBase.ReflectedType.Name+"."+methodBase.Name+":" ;
        }

		/// <summary>
		/// 프리셋에서 불러온 메뉴들을 반납
		/// </summary>
		/// <param name="f_prest"></param>
		/// <returns></returns>
		public static MaidProp[] getMaidProp(CharacterMgr.Preset f_prest)
		{
			// f_prest.strFileName
			//
			MaidProp[] array;
			if (f_prest.ePreType == CharacterMgr.PresetType.Body)
			{
				array = (from mp in f_prest.listMprop
						 where (1 <= mp.idx && mp.idx <= 80) || (115 <= mp.idx && mp.idx <= 122)
						 select mp).ToArray<MaidProp>();
			}
			else if (f_prest.ePreType == CharacterMgr.PresetType.Wear)
			{
				array = (from mp in f_prest.listMprop
						 where 81 <= mp.idx && mp.idx <= 110
						 select mp).ToArray<MaidProp>();
			}
			else
			{
				array = (from mp in f_prest.listMprop
						 where (1 <= mp.idx && mp.idx <= 110) || (115 <= mp.idx && mp.idx <= 122)
						 select mp).ToArray<MaidProp>();
			}

			return array;
		}

	}
}
