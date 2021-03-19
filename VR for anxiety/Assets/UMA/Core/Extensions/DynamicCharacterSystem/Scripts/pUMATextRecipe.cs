using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using UMA.CharacterSystem;

namespace UMA
{
	public partial class UMATextRecipe : UMAPackedRecipeBase
	{
		//TODO use the recipeTypeOpts enum for this everywhere
		public string recipeType = "Standard";

		[SerializeField]
		public string DisplayValue; // some sort of text value.

		[SerializeField]
		public List<string> compatibleRaces = new List<string>();

		[SerializeField]
		public List<WardrobeRecipeThumb> wardrobeRecipeThumbs = new List<WardrobeRecipeThumb>();

		public string wardrobeSlot = "None";

		[SerializeField]
		public List<string> Hides = new List<string>();

		[SerializeField]
		public List<string> HideTags = new List<string>();

		[SerializeField]
		public List<string> suppressWardrobeSlots = new List<string>();

		[SerializeField]
		public List<WardrobeSettings> activeWardrobeSet = new List<WardrobeSettings>();//used in the editor to draw 'DynamicCharacterSystem' type recipe assets, in a different way to 'Standard' or 'Wardrobe' assets

        [SerializeField]
        public List<MeshHideAsset> MeshHideAssets = new List<MeshHideAsset>();

       // [SerializeField]
		//public string AddressableLabel;


#if UNITY_EDITOR
		/// <summary>
		/// Converts this recipe to the given child type. Used by RecipeEditor to convert old recipes
		/// </summary>
		public virtual void ConvertToType(string typeName)
		{
			if (Debug.isDebugBuild)
				Debug.Log("Tried to convert to " + typeName);
			foreach (Type t in Assembly.GetAssembly(typeof(UMATextRecipe)).GetTypes())
			{
				if (t.Name == typeName)
				{
					if (Debug.isDebugBuild)
						Debug.Log("found matching type");
					MethodInfo ConvertMethod = t.GetMethod("ConvertFromUTR", BindingFlags.Instance | BindingFlags.NonPublic);
					if (ConvertMethod != null)
					{
						if (Debug.isDebugBuild)
							Debug.Log("Found Convert method");
						var newT = ScriptableObject.CreateInstance(t);
						ConvertMethod.Invoke(newT, new object[] { this, true });
						break;
					}
					else
					{
						if (Debug.isDebugBuild)
							Debug.Log("No convert method found in type " + t.Name);
					}
				}
			}
		}

#endif

#if UNITY_EDITOR
		/// <summary>
		/// Creates a temporary UMAContextBase for use when editing recipes when the open Scene does not have an UMAContextBase or libraries set up
		/// </summary>
		/// 
		public override UMAContextBase CreateEditorContext()
		{
			UMAContextBase.CreateEditorContext();
			return UMAContextBase.Instance;
		}
#endif


		/// <summary>
		/// Gets the thumbnail for this WardrobeRecipe filtered by racename
		/// </summary>
		/// <param name="racename"></param>
		/// <returns></returns>
		public Sprite GetWardrobeRecipeThumbFor(string racename)
		{
			Sprite foundSprite = null;
			if (wardrobeRecipeThumbs.Count > 0)
			{
				foreach (WardrobeRecipeThumb wdt in wardrobeRecipeThumbs)
				{
					//Set a default when the first option with a value is found
					if (foundSprite == null && wdt.thumb != null)
					{
						foundSprite = wdt.thumb;
					}
					//Override that if there is a specific one is found for this race
					if (wdt.race == racename)
					{
						if (wdt.thumb != null)
						{
							foundSprite = wdt.thumb;
						}
					}
				}
			}
			//this could be extended to generate a sprite/texture if the recipe only has a filename but has no sprite refrence (i.e. if it was generated by the user)
			return foundSprite;
		}
		public static List<WardrobeSettings> GenerateWardrobeSet(Dictionary<string, UMATextRecipe> wardrobeRecipes, Dictionary<string, UMAWardrobeCollection> wardrobeCollections, params string[] slotsToSave)
		{
			var thisWardrobeSettings = GenerateWardrobeSet(wardrobeRecipes, slotsToSave);
			foreach(UMAWardrobeCollection uwr in wardrobeCollections.Values)
			{
				thisWardrobeSettings.Add(new WardrobeSettings("WardrobeCollection", uwr.name));
			}
			return thisWardrobeSettings;
		}
		public static List<WardrobeSettings> GenerateWardrobeSet(Dictionary<string, UMATextRecipe> wardrobeRecipes, params string[] slotsToSave)
		{
			if (wardrobeRecipes == null)
				return null;
			var wardrobeSet = new List<WardrobeSettings>();
			if (wardrobeRecipes.Count == 0)
				return wardrobeSet;
			if (slotsToSave.Length > 0)
			{
				foreach (string s in slotsToSave)
				{
					if (wardrobeRecipes.ContainsKey(s))
					{
						UMATextRecipe utr = wardrobeRecipes[s];
						if (utr != null)
						{
							wardrobeSet.Add(new WardrobeSettings(s, utr.name));
							continue;
						}
					}
					wardrobeSet.Add(new WardrobeSettings(s, ""));
				}
			}
			else
			{
				foreach (KeyValuePair<string, UMATextRecipe> kp in wardrobeRecipes)
					wardrobeSet.Add(new WardrobeSettings(kp.Key, kp.Value.name));
			}
			return wardrobeSet;
		}

		//Override Load from PackedRecipeBase
		/// <summary>
		/// Load this Recipe's recipeString into the specified UMAData.UMARecipe. If there is Wardrobe data in the recipe string, its values are set to this recipe assets 'activeWardrobeSet' field
		/// </summary>
		/// <param name="umaRecipe">UMA recipe.</param>
		/// <param name="context">Context.</param>
		public override void Load(UMA.UMAData.UMARecipe umaRecipe, UMAContextBase context = null)
		{
			//This check can be removed in future- If we set the recipeType properly from now on we should not need to do this check
			var typeInRecipe = GetRecipesType(recipeString);
			recipeType = typeInRecipe != "Standard" ? typeInRecipe : recipeType;
			if (RecipeHasWardrobeSet(recipeString))
				activeWardrobeSet = GetRecipesWardrobeSet(recipeString);
			//if its an old UMARecipe there wont be an activeWardrobeSet field
			if (activeWardrobeSet == null)
			{
				recipeType = "Standard";
				base.Load(umaRecipe, context);
				return;
			}
			//if it has a wardrobeSet or was saved using the DCSPackRecipe Model
			if (activeWardrobeSet.Count > 0 || (recipeType == "DynamicCharacterAvatar" /*|| recipeType == "WardrobeCollection"*/))
			{
				var packedRecipe = PackedLoadDCSInternal(context/*, recipeString*/);
			   UnpackRecipe(umaRecipe, packedRecipe, context);
			}
			else //we can use standard UMALoading
			{
				base.Load(umaRecipe, context);
			}
		}

		/// <summary>
		/// Internal call to static PackedLoadDCS which uses the assets string and object and returns a DCSUniversalPackRecipe data model that can be used by any UMA
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		protected DCSUniversalPackRecipe PackedLoadDCSInternal(UMAContextBase context/*, string recipeToUnpack*/)
		{
			return PackedLoadDCS(context, recipeString, this);
		}

		/// <summary>
		/// Returns the recipe string as a DCSUniversalPackRecipe data model that can be used by any UMA
		/// </summary>
		/// <param name="context"></param>
		/// <param name="recipeToUnpack"></param>
		/// <param name="targetUTR">If set the wardrobeSet (if it exists) and the recipeType will assigned to UMATextRecipe assets fields (used by the Recipe Editor)</param>
		/// <returns></returns>
		public static DCSUniversalPackRecipe PackedLoadDCS(UMAContextBase context, string recipeToUnpack, UMATextRecipe targetUTR = null)
		{
			if ((recipeToUnpack == null) || (recipeToUnpack.Length == 0))
				return new DCSUniversalPackRecipe();
			//first use the DCSRecipeChecker to check if this is a DCS recipe
			var typeInRecipe = GetRecipesType(recipeToUnpack);
			var targetRecipeType = typeInRecipe != "Standard" ? typeInRecipe : (targetUTR != null ? targetUTR.recipeType : "Standard");
			if (targetUTR != null)
			{
				targetUTR.recipeType = targetRecipeType;
				if (RecipeHasWardrobeSet(recipeToUnpack))
					targetUTR.activeWardrobeSet = GetRecipesWardrobeSet(recipeToUnpack);
			}
			//Right now the only recipeType that uses the DCSModel is "DynamicCharacterAvatar"
			DCSUniversalPackRecipe thisUnpackedUniversal = null;
			if (targetRecipeType == "DynamicCharacterAvatar" || targetRecipeType == "WardrobeCollection")
			{
				var thisUnpacked = JsonUtility.FromJson<DCSPackRecipe>(recipeToUnpack);
				thisUnpackedUniversal = new DCSUniversalPackRecipe(thisUnpacked);
			}
			else
			{
				var thisUnpacked = JsonUtility.FromJson<UMAPackRecipe>(recipeToUnpack);
				thisUnpackedUniversal = new DCSUniversalPackRecipe(thisUnpacked);
			}
			if (RecipeHasWardrobeSet(recipeToUnpack))
				thisUnpackedUniversal.wardrobeSet = GetRecipesWardrobeSet(recipeToUnpack);

			return thisUnpackedUniversal;
		}

		//TODO: once everyone has their recipes updated remove this- we only want UMATextRecipes to save as 'Standard'
		/// <summary>
		/// Saves a 'Standard' UMATextRecipe. If saving a DynamicCharacterAvatar as 'Backwards Compatible' this will save a recipe that has slots/overlay data AND a wardrobe set
		/// </summary>
		public void Save(UMAData.UMARecipe umaRecipe, UMAContextBase context, Dictionary<string, UMATextRecipe> wardrobeRecipes, bool backwardsCompatible = true)
		{
			if (wardrobeRecipes.Count > 0)
				activeWardrobeSet = GenerateWardrobeSet(wardrobeRecipes);
			recipeType = backwardsCompatible ? "Standard" : "DynamicCharacterAvatar";
			Save(umaRecipe, context);
		}

		//This is used when an inspected recipe asset is saved
		public override void Save(UMAData.UMARecipe umaRecipe, UMAContextBase context)
		{
			if (recipeType == "Wardrobe")//Wardrobe Recipes can save the standard UMA way- they dont have WardrobeSets- although the recipe string wont have a packedRecipeType field
			{
				base.Save(umaRecipe, context);
			}
			else if (recipeType != "Standard")//this will just be for type DynamicCharacterAvatar- and WardrobeCollection if we add that
			{
				var packedRecipe = PackRecipeV3(umaRecipe);
				//DCSPackRecipe doesn't do any more work, it just gets the values from PackRecipeV3 that we need and discards the rest
				var packedRecipeToSave = new DCSPackRecipe(packedRecipe, this.name, recipeType, activeWardrobeSet);
				recipeString = JsonUtility.ToJson(packedRecipeToSave);
			}
			else //This will be Standard- this is 'backwards Compatible' and is also how the Recipe Editor saves 'backwardsCompatible' 'Standard' recipes when they are inspected
			{
				umaRecipe.MergeMatchingOverlays();
				var packedRecipe = PackRecipeV3(umaRecipe);
				var packedRecipeToSave = new DCSUniversalPackRecipe(packedRecipe);//this gets us a recipe with all the standard stuff plus our extra fields
																				//so now we can save the wardrobeSet into it if it existed
				if (activeWardrobeSet != null)
					if (activeWardrobeSet.Count > 0)
					{
						packedRecipeToSave.wardrobeSet = activeWardrobeSet;
					}
				recipeString = JsonUtility.ToJson(packedRecipeToSave);
			}
		}

		//This is the save method used by DCA.DoSave and the 'optimized' UMA Menu save options

		/// <summary>
		/// Save the DynamicCharacterAvatar using the optimized DCSPackRecipe model (Not compatible with non-DynamicCharacterAvatars)
		/// </summary>
		/// <param name="dcaToSave"></param>
		/// <param name="recipeName"></param>
		/// <param name="saveOptions">Set the save flags options to choose which properties of the Avatar to save</param>
		public void SaveDCS(DynamicCharacterAvatar dcaToSave, string recipeName, DynamicCharacterAvatar.SaveOptions saveOptions)
		{
			recipeString = JsonUtility.ToJson(new DCSPackRecipe(dcaToSave, recipeName, "DynamicCharacterAvatar", saveOptions));
		}

		/// <summary>
		/// Super lightweight model used for quickly checking the recipe type of a given recipeString and whether the recipe has and wardrobe data. Use the helper methods below for easy access
		/// </summary>
		private class DCSRecipeChecker
		{
			public string packedRecipeType = "Standard";
			//we can have backwards compatibility here too
			public List<WardrobeSettings> wardrobeRecipesJson = new List<WardrobeSettings>();
			public List<WardrobeSettings> wardrobeSet = new List<WardrobeSettings>();
			public List<WardrobeSettings> checkedWardrobeSet
			{
				get
				{
					if (wardrobeSet.Count > 0)
						return wardrobeSet;
					else
						return wardrobeRecipesJson;
				}
			}
		}

		//Helper Methods for use with DCSRecipeChecker

		/// <summary>
		/// Get the recipeType of the given recipeString
		/// </summary>
		/// <param name="recipeString"></param>
		/// <returns></returns>
		public static string GetRecipesType(string recipeString)
		{
			if (String.IsNullOrEmpty(recipeString))
				return "Standard";
			return JsonUtility.FromJson<DCSRecipeChecker>(recipeString).packedRecipeType;
		}
		/// <summary>
		/// Check if the given recipe string has any wardrobeSet data
		/// </summary>
		/// <param name="recipeString"></param>
		/// <returns></returns>
		public static bool RecipeHasWardrobeSet(string recipeString)
		{
			if (String.IsNullOrEmpty(recipeString))
				return false;
			return JsonUtility.FromJson<DCSRecipeChecker>(recipeString).checkedWardrobeSet.Count > 0;
		}
		/// <summary>
		/// Get the wardrobeSet data from the given recipe string
		/// </summary>
		/// <param name="recipeString"></param>
		/// <returns></returns>
		public static List<WardrobeSettings> GetRecipesWardrobeSet(string recipeString)
		{
			if (String.IsNullOrEmpty(recipeString))
				return null;
			return JsonUtility.FromJson<DCSRecipeChecker>(recipeString).checkedWardrobeSet;
		}

		[System.Serializable]
		public class DCSPackRecipe
		{
			public string packedRecipeType = "DynamicCharacterAvatar";
			public string name;
			public string race;
			public List<UMAPackedDna> dna;
			public List<PackedOverlayColorDataV3> characterColors;
			public List<WardrobeSettings> wardrobeSet;
			public string raceAnimatorController;

			private OverlayColorData[] _sharedColors = null;

			#region PUBLIC PROPERTIES

			public OverlayColorData[] sharedColors
			{
				get
				{
					if (_sharedColors == null)
					{
						if (characterColors != null && characterColors.Count > 0)
						{
							var colorData = new OverlayColorData[characterColors.Count];
							for (int i = 0; i < colorData.Length; i++)
							{
								colorData[i] = new OverlayColorData();
								characterColors[i].SetOverlayColorData(colorData[i]);
							}
							_sharedColors = colorData;
						}
						else
						{
							_sharedColors = new OverlayColorData[0];
						}
					}
					return _sharedColors;
				}
			}

			#endregion

			#region CONSTRUCTOR
			/// <summary>
			/// This is the main DCS Data model now. When a DCS is saved it is saved using this
			/// </summary>
			public DCSPackRecipe() { }
			/// <summary>
			/// Use this model for saving a DCS Avatar to a light weight json string. Use the save options flags to determine what aspects of the avatar are saved
			/// </summary>
			/// <param name="dcaToSave"></param>
			/// <param name="recipeName"></param>
			/// <param name="pRecipeType"></param>
			/// <param name="saveOptions"></param>
			/// <param name="slotsToSave"></param>
			public DCSPackRecipe(DynamicCharacterAvatar dcaToSave, string recipeName, string pRecipeType, DynamicCharacterAvatar.SaveOptions saveOptions, params string[] slotsToSave)
			{
				if (pRecipeType != "DynamicCharacterAvatar")
				{
					Debug.LogWarning("DCSPackRecipe Type can only be used for recipeTypes 'DynamicCharacterAvatar'");
					return;
				}
				var recipeToSave = dcaToSave.umaData.umaRecipe;
				packedRecipeType = pRecipeType;
				name = recipeName;
				race = dcaToSave.activeRace.racedata.raceName;
				if (saveOptions.HasFlagSet(DynamicCharacterAvatar.SaveOptions.saveDNA))
					dna = GetPackedDNA(recipeToSave);
				if (saveOptions.HasFlagSet(DynamicCharacterAvatar.SaveOptions.saveColors))
				{
					characterColors = new List<PackedOverlayColorDataV3>();
					for (int i = 0; i < recipeToSave.sharedColors.Length; i++)
					{
						characterColors.Add(new PackedOverlayColorDataV3(recipeToSave.sharedColors[i]));
					}
				}
				if (saveOptions.HasFlagSet(DynamicCharacterAvatar.SaveOptions.saveWardrobe))
					wardrobeSet = GenerateWardrobeSet(dcaToSave.WardrobeRecipes, dcaToSave.WardrobeCollections, slotsToSave);
				if (saveOptions.HasFlagSet(DynamicCharacterAvatar.SaveOptions.saveAnimator))
				{
					if (dcaToSave.animationController != null)
						raceAnimatorController = (dcaToSave.animationController.name);
				}
			}
			/// <summary>
			/// Converts standard UMA Pack Recipe data into this data model, either when loading an old recipe, or when the RecipeEditor is saving a DCS recipe asset
			/// </summary>
			/// <param name="umaPackRecipe"></param>
			/// <param name="recipeName"></param>
			/// <param name="pRecipeType"></param>
			/// <param name="wardrobeSetToSave"></param>
			public DCSPackRecipe(UMAPackRecipe umaPackRecipe, string recipeName = "", string pRecipeType = "Standard", List<WardrobeSettings> wardrobeSetToSave = null)
			{
				//Debug.Log("DCSPackRecipe from umaPackRecipe");
				packedRecipeType = pRecipeType;
				name = recipeName != "" ? recipeName : umaPackRecipe.race + "PackRecipe";//- if its coming from the recipe editor the we wont want to change a name
				race = umaPackRecipe.race;
				dna = umaPackRecipe.packedDna;
				characterColors = new List<PackedOverlayColorDataV3>(umaPackRecipe.fColors);
				//if this is coming from the Recipe Editor then it will need wardrobe settings- but it cant send them- however they will be in the activeWardrobeSet field
				if (wardrobeSetToSave != null)
					wardrobeSet = wardrobeSetToSave;
			}
			#endregion
		}

		//This is a Universal model that makes the new DCSModel 'backwards compatible' so the data can be used in the inspector- its only used at runtime and not saved unless you save with the 'backwards Compatible' save options
		[System.Serializable]
		public class DCSUniversalPackRecipe : UMAPackRecipe
		{
			[SerializeField]
			public List<WardrobeSettings> wardrobeSet = null;

			public string packedRecipeType = "Standard";

			private OverlayColorData[] _sharedColors = null;

			public OverlayColorData[] sharedColors
			{
				get
				{
					if (_sharedColors == null)
					{
						//PackedOverlayColorDataV3
						if (fColors != null)
						{
							if (fColors.Length > 0)
							{
								var colorData = new OverlayColorData[fColors.Length];
								for (int i = 0; i < colorData.Length; i++)
								{
									colorData[i] = new OverlayColorData();
									fColors[i].SetOverlayColorData(colorData[i]);
								}
								_sharedColors = colorData;
							}
							else
								_sharedColors = new OverlayColorData[0];
						}//PackedOverlayColorDataV3
						else if (colors != null)
						{
							if (colors.Length > 0)
							{
								var colorData = new OverlayColorData[colors.Length];
								for (int i = 0; i < colorData.Length; i++)
								{
									colorData[i] = new OverlayColorData();
									colors[i].SetOverlayColorData(colorData[i]);
								}
								_sharedColors = colorData;
							}
							else
								_sharedColors = new OverlayColorData[0];
						}
						else
						{
							_sharedColors = new OverlayColorData[0];
						}
					}
					return _sharedColors;
				}
			}
			#region CONSTRUCTORS

			public DCSUniversalPackRecipe()
			{

			}
			/// <summary>
			/// Convert an UMAPackRecipe into a DCSUniversalPackRecipe. Used when DCS needs to load an old UMA
			/// </summary>
			/// <param name="umaPackRecipe"></param>
			/// <param name="pRecipeType"></param>
			public DCSUniversalPackRecipe(UMAPackRecipe umaPackRecipe, string pRecipeType = "Standard")
			{
				//Debug.Log("Created universal model from UMAPackRecipe");
				packedRecipeType = pRecipeType;
				version = umaPackRecipe.version;
				packedSlotDataList = umaPackRecipe.packedSlotDataList;
				slotsV3 = umaPackRecipe.slotsV3;
				colors = umaPackRecipe.colors;
				fColors = umaPackRecipe.fColors;
				sharedColorCount = umaPackRecipe.sharedColorCount;
				race = umaPackRecipe.race;
				umaDna = umaPackRecipe.umaDna;
				packedDna = umaPackRecipe.packedDna;
			}
			/// <summary>
			/// This is used when an avatar asset, saved using the DCS Model, is loaded for editing in the Inspector
			/// </summary>
			/// <param name="dcsPackRecipe"></param>
			public DCSUniversalPackRecipe(DCSPackRecipe dcsPackRecipe)
			{
				//Debug.Log("Created universal model from DCSPackRecipe");
				packedRecipeType = dcsPackRecipe.packedRecipeType;
				version = 3;
				slotsV3 = new PackedSlotDataV3[0];//we need this otherwise the RecipeInspector doesn't show anything...
				race = dcsPackRecipe.race;
				fColors = dcsPackRecipe.characterColors.ToArray();
				sharedColorCount = dcsPackRecipe.characterColors.Count;
				packedDna = dcsPackRecipe.dna;
				wardrobeSet = dcsPackRecipe.wardrobeSet;
			}
			/// <summary>
			/// This is used to save a DCS avatar to a 'backwards compatible' UMA model
			/// </summary>
			/// <param name="recipeToSave"></param>
			/// <param name="wardrobeRecipes"></param>
			/// <param name="pRecipeType"></param>
			public DCSUniversalPackRecipe(UMAData.UMARecipe recipeToSave, Dictionary<string, UMATextRecipe> wardrobeRecipes = null, string pRecipeType = "DynamicCharacterAvatar")
			{
				//Debug.Log("Created universal model from Avatar");
				var packedRecipe = PackRecipeV3(recipeToSave);
				packedRecipeType = pRecipeType;
				version = packedRecipe.version;
				packedSlotDataList = packedRecipe.packedSlotDataList;
				slotsV3 = packedRecipe.slotsV3;
				colors = packedRecipe.colors;
				fColors = packedRecipe.fColors;
				sharedColorCount = packedRecipe.sharedColorCount;
				race = packedRecipe.race;
				umaDna = packedRecipe.umaDna;
				packedDna = packedRecipe.packedDna;
				wardrobeSet = GenerateWardrobeSet(wardrobeRecipes);
			}
			#endregion

			#region METHODS
			public UMADnaBase[] GetAllDna()
			{
				List<UMADnaBase> unpackedDna = UMAPackedRecipeBase.UnPackDNA(packedDna);
				return unpackedDna.ToArray();
			}
			#endregion
		}
	}
}
