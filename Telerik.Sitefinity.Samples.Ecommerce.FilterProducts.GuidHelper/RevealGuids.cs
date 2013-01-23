using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Samples.Ecommerce.FilterProducts.GuidHelper
{
    public class RevealGuids : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets the layout template path
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                return RevealGuids.layoutTemplatePath;
            }
        }

        /// <summary>
        /// Gets the layout template name
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return String.Empty;
            }
        }
        #endregion

        #region Control References
        /// <summary>
        /// Reference to the repeater control that shows the department guids.
        /// </summary>
        protected virtual Repeater DepartmentRepeater
        {
            get
            {
                return this.Container.GetControl<Repeater>("rptDepartments", true);
            }
        }

        /// <summary>
        /// Reference to the repeater control that shows the product type guids.
        /// </summary>
        protected virtual Repeater ProductTypesRepeater
        {
            get
            {
                return this.Container.GetControl<Repeater>("rptProductTypes", true);
            }
        }

        /// <summary>
        /// Reference to the repeater control that shows the tag guids.
        /// </summary>
        protected virtual Repeater TagsRepeater
        {
            get
            {
                return this.Container.GetControl<Repeater>("rptTags", true);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            this.DepartmentRepeater.DataSource = GetDepartments();
            this.DepartmentRepeater.ItemDataBound += Repeater_ItemDataBound;
            this.DepartmentRepeater.DataBind();

            this.ProductTypesRepeater.DataSource = GetProductTypes();
            this.ProductTypesRepeater.ItemDataBound += Repeater_ItemDataBound;
            this.ProductTypesRepeater.DataBind();

            this.TagsRepeater.DataSource = GetTags();
            this.TagsRepeater.ItemDataBound += Repeater_ItemDataBound;
            this.TagsRepeater.DataBind();
        }

        #endregion

        protected List<GuidDataViewModel> GetProductTypes()
        {
            List<GuidDataViewModel> data = new List<GuidDataViewModel>();
            CatalogManager catalogManager = CatalogManager.GetManager();

            var productTypes = catalogManager.GetProductTypes();

            if (productTypes == null)
            {
                return data;
            }

            foreach (var productType in productTypes)
            {
                string title = productType.Title;
                Guid id = productType.Id;

                var dataModel = new GuidDataViewModel { Id = id, Title = title };
                data.Add(dataModel);
            }

            return data;
        }

        protected List<GuidDataViewModel> GetTags()
        {
            List<GuidDataViewModel> data = new List<GuidDataViewModel>();
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();

            FlatTaxonomy taxonData = taxonomyManager.GetTaxonomies<FlatTaxonomy>().Where(t => t.Name == "Tags").SingleOrDefault();

            return GetTaxonData(data, taxonData);
        }

        protected List<GuidDataViewModel> GetDepartments()
        {
            List<GuidDataViewModel> data = new List<GuidDataViewModel>();
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            
            HierarchicalTaxonomy taxonData = taxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Where(t => t.Name == "Departments").SingleOrDefault();

            return GetTaxonData(data, taxonData);
        }

        private List<GuidDataViewModel> GetTaxonData(List<GuidDataViewModel> data, ITaxonomy taxonData)
        {
            if (taxonData == null)
            {
                return data;
            }

            foreach (var taxa in taxonData.Taxa)
            {
                string title = taxa.Title.Value;
                Guid id = taxa.Id;

                var dataModel = new GuidDataViewModel { Id = id, Title = title };
                data.Add(dataModel);
            }

            return data;
        }

        protected internal virtual void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var guidData = e.Item.DataItem as GuidDataViewModel;

                var nameLabel = e.Item.FindControl("lblName") as Label;
                if (nameLabel != null)
                {
                    nameLabel.Text = guidData.Title;
                }

                var idLabel = e.Item.FindControl("lblId") as Label;
                if (idLabel != null)
                {
                    idLabel.Text = guidData.Id.ToString();
                }
            }
        }

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/TelerikSitefinitySamplesEcommerceFilterProductsGuidHelper/Telerik.Sitefinity.Samples.Ecommerce.FilterProducts.GuidHelper.RevealGuids.ascx";
        #endregion
    }
}
