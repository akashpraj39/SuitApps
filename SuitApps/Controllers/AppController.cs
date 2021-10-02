using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuitApps.Models.ModelClass;
using SuitApps.Models.Repository;

namespace SuitApps.Controllers
{
    public class AppController : ApiController
    {
        #region Login
        [HttpPost]
        [ActionName("Login")]
        public LoginResponse Post([FromBody]LoginRequest loginRequest)
        {
            return LoginRepository.Login(loginRequest);
        }

        [HttpGet]
        [ActionName("GetAllCompanyNames")]
        public CompanyInfoList Get()
        {
            return LoginRepository.GetAllCompanyNames();
        }

        [HttpPost]
        [ActionName("GetEmployeeRoute_DayWise")]
        public EmployeeRoute Post([FromBody]EmployeeRoute employeeRoute)
        {
            return LoginRepository.GetEmployeeRoute_DayWise(employeeRoute);
        }

        [HttpPost]
        [ActionName("RouteList")]
        public RouteList RouteList([FromBody]EmployeeRoute employeeRoute)
        {
            return LoginRepository.GetEmployee_MultiRoute_DayWise(employeeRoute);
        } 

        #endregion

        #region Customer
        [HttpPost]
        [ActionName("GetAllCustomers_Root_Day_Wise")]
        public CustomerList Get(EmployeeRoute empRoute)
        {
            return CustomerRepository.GetAllCustomers_Root_Day_Wise(empRoute);
        }

        [HttpPost]
        [ActionName("GetAllCustomers_MultiRoot_Day_Wise")]
        public CustomerList GetAllCustomers_MultiRoot_Day_Wise(RouteList empRoute)
        {
            return CustomerRepository.GetAllCustomers_MultiRoot_Day_Wise(empRoute);
        }

        [HttpPost]
        [ActionName("GetAllCustomersWithPagination")]
        public CustomerList GetAllCustomersWithPagination(EmployeeRoute empRoute)
        {
            return CustomerRepository.PaginationForCustomer(empRoute);
        }

        [HttpPost]
        [ActionName("SearchCustomer")]
        public CustomerList SearchCustomer(EmployeeRoute empRoute)
        {
            return CustomerRepository.SearchCustomer(empRoute);
        }

        [HttpPost]
        [ActionName("GetCustomerDetailById")]
        public Customer GetCustomerDetailById(Customer customer)
        {
            return CustomerRepository.GetCustomerDetailById(customer);
        }
        [HttpPost]
        [ActionName("ManageCustomer")]
        public Account ManageCustomer(Account account)
        {
            return CustomerRepository.ManageCustomer(account);
        }

      



        #endregion

        #region Product
        [HttpGet]
        [ActionName("GetAllProducts_App")]
        public ProductList GetAllProducts()
        {
            return ProductRepository.GetAllProducts_App();
        }

        [HttpGet]
        [ActionName("GetVanLoad")]
        public ProductList GetVanItems(int VanID)
        {
            return ProductRepository.GetVanItems(VanID);
        }
        [HttpPost]
        [ActionName("GetAllProductsWithPagination")]
        public ProductList GetAllProductsWithPagination(Pagination page)
        {
            return ProductRepository.GetAllProducts_AppWithPagination(page);
        }
        [HttpGet]
        [ActionName("GetProductDetailById")]
        public Product GetProductDetailById(int itemId)
        {
            return ProductRepository.GetProductDetailById(itemId);
        }
        [HttpGet]
        [ActionName("GetStock")]
        public Product GetStock(int itemId)
        {
            return ProductRepository.GetStock(itemId);
        }
        [HttpGet]
        [ActionName("SearchProduct")]
        public ProductList SearchProduct(string ItemName)
        {
            return ProductRepository.SearchProduct(ItemName);
        }
        
        #endregion

        #region SaleOrder

        [HttpGet]
        [ActionName("SaleOrderAutogenerate")]
        public SaleOrder SaleOrderAutogenerate()
        {
            return SaleOrderRepository.SaleOrderAutogenerate();
        }
        [HttpPost]
        [ActionName("InsertSaleOrder_App")]
        public SaleOrder InsertSaleOrder_App(SaleOrder saleOrder)
        {
            return SaleOrderRepository.InsertSaleOrder_App(saleOrder);
        }
        [HttpPost]
        [ActionName("ItemPurchase")]
        public SaleOrderDetailList InsertSalesOrderDetail(SaleOrderDetailList sale)
        {
            return SaleOrderDetailRepository.InsertSalesOrderDetails(sale);
        }
        [HttpGet]
        [ActionName("GetAllItemPurchase")]
        public SaleOrderDetailList GetAllItemPurchase(int Sodid)
        {
            return SaleOrderDetailRepository.GetAllFrom_SalesOrderDetails(Sodid);
        }
        [HttpPost]
        [ActionName("SaleOrderListing")]
        public SaleForCustomerList SaleOrderListing(EmployeeRoute emp)
        {
            return SaleOrderRepository.SaleOrderListing_ordered(emp);
        }
        [HttpGet]
        [ActionName("SaleOrderByOrderNo")]
        public SaleForCustomer SaleOrderByOrderNo(string OrderNo)
        {
            return SaleOrderRepository.SaleOrderByOrderNo(OrderNo);
        }
        [HttpGet]
        [ActionName("AmendSale")]
        public SaleForCustomerList SaleOrderOfcustomer_ordered(int soid)
        {
            return SaleOrderRepository.SaleOrderOfcustomer_ordered(soid);
        }
      

        #endregion

        #region DirectSales

        [HttpGet]
        [ActionName("DirectSaleIDAutogenerate")]
        public DirectSales DirectSaleIDAutogenerate()
        {
            return DirectSalesRepository.DirectSaleIDAutogenerate();
        }
        [HttpPost]
        [ActionName("InsertDirectSales_App")]
        public DirectSales InsertDirectSales_App(DirectSales directSales)
        {
            return DirectSalesRepository.InsertDirectSales_App(directSales);
        }
        [HttpPost]
        [ActionName("DirectSalePurchase")]
        public DirectSalesDetailList InsertDirectSalesDetails(DirectSalesDetailList sale)
        {
            return DirectSaleDetailRepository.InsertDirectSalesDetails(sale);
        }
        [HttpGet]
        [ActionName("GetAllDirectSalePurchase")]
        public DirectSalesDetailList GetAllFrom_DirectSalesDetails(int Dsdid)
        {
            return DirectSaleDetailRepository.GetAllFrom_DirectSalesDetails(Dsdid);
        }
        [HttpPost]
        [ActionName("DirectSalesListing_ordered")]
        public DirectSalesForCustomerList DirectSalesListing_ordered(EmployeeRoute emp)
        {
            return DirectSalesRepository.DirectSalesListing_ordered(emp);
        }
        [HttpGet]
        [ActionName("DirectSalesByOrderNo")]
        public DirectSalesForCustomer DirectSalesByOrderNo(string OrderNo)
        {
            return DirectSalesRepository.DirectSalesByOrderNo(OrderNo);
        }
        [HttpGet]
        [ActionName("AmendDirectSale")]
        public DirectSalesForCustomerList DirectSalesOfcustomer_ordered(int dsid)
        {
            return DirectSalesRepository.DirectSalesOfcustomer_ordered(dsid);
        }


        #endregion

        #region Synchronization
        #region Sync : Customer
        [HttpPost]
        [ActionName("Sync_ManageCustomer")]
        public AccountList Sync_NewCustomer([FromBody]AccountList accounts)
        {
            return Sync_ManageCustomer.Sync_NewCustomer(accounts);
        }
        #endregion

      
        #region Sync : Attendance
        [HttpPost]
        [ActionName("Insert_Update_Attendance")]
        public AttendanceList Sync_Attendance([FromBody]AttendanceList attendance)
        {
            return AttendanceRepository.Sync_Attendance(attendance);
        }
        #endregion

        #region Sync : SaleOrder
        [HttpPost]
        [ActionName("Sync_SaleOrders")]
        public SaleOrderList Sync_SaleOrder(SaleOrderList saleOrder)
        {
            return Sync_SaleOrdersReprository.Sync_SaleOrder(saleOrder);
        }
        [HttpPost]
        [ActionName("Sync_SaleOrderDetails")]
        public SaleOrderDetailList Sync_SaleOrderDetails(SaleOrderDetailList sale)
        {
            return Sync_SaleOrdersReprository.Sync_SaleOrderDetails(sale);
        } 
        #endregion

        #region Sync : DirectSale
        [HttpPost]
        [ActionName("Sync_DirectSales")]
        public DirectSalesList Sync_DirectSales(DirectSalesList directSales)
        {
            return Sync_DirectSalesReprository.Sync_DirectSalesData(directSales);
        }
        [HttpPost]
        [ActionName("Sync_DirectSalesDetails")]
        public DirectSalesDetailList Sync_DirectSalesDetails(DirectSalesDetailList sale)
        {
            return Sync_DirectSalesReprository.Sync_DirectSalesDetails(sale);
        } 
        #endregion
        #endregion

        #region Count
        [HttpGet]
        [ActionName("GetProductCount")]
        public Count GetProductCount()
        {
            return CountRepository.GetProductCount();
        }
        #endregion

        #region State
        [HttpGet]
        [ActionName("BindState")]
        public StateList BindState()
        {
            return StateRepository.BindState();
        }

        #endregion

        #region Districts
        [HttpGet]
        [ActionName("BindDistrict")]
        public StateList BindDistrict()
        {
            return StateRepository.BindDistrict();
        }
        #endregion

        #region Taluk
        [HttpGet]
        [ActionName("BindTaluk")]
        public StateList BindTaluk()
        {
            return StateRepository.BindTaluk();
        }
        #endregion

        #region EmployeeDistributor
        [HttpPost]
        [ActionName("GetDistributor_RootWise")]
      
        public EmpDisList GetDistributor_RootWise(EmpDistributor empDis)
        {
            return EmpDistributorRepository.GetDistributor_RootWise(empDis);
        }
        #endregion

        #region EmpLocation
        [HttpPost]
        [ActionName("InsertEmpLocation")]
        public EmpLocationList InsertEmpLocation(EmpLocationList emploclist)
        {
            return EmpLocationRepository.InsertEmpLocation(emploclist);
        }
        #endregion

        #region ItemStockReturn
        [HttpPost]
        [ActionName("Sync_ItemStockReturn")]
        public ItemStockReturnList Sync_ItemStockReturn(ItemStockReturnList itemstockreturn)
        {
            return ItemStockReturnRepository.Insert_ItemStockReturn(itemstockreturn);

        }
        #endregion

      

    }
}