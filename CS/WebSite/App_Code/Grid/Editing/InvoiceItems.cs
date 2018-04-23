using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.ComponentModel;
using System.Web.SessionState;

public class InvoiceItem {
    IList owner;
    string name;
    decimal price;
    int quantity;
    bool isPaid;

    public InvoiceItem(IList owner) {
        this.owner = owner;
        this.price = 0;
        this.quantity = 1;
    }
    public string Name { get { return name; } set { name = value; } }
    public decimal Price { get { return price; } set { price = value; } }
    public int Quantity { get { return quantity; } set { quantity = value; } }
    public int Id { get { return owner != null ? owner.IndexOf(this) + 1 : -1; } }
    public bool IsPaid { get { return isPaid; } set { isPaid = value; } }
    public decimal Total { get { return Price * Quantity; } }
}

public class InvoiceItemsProvider {
    HttpSessionState Session { get { return HttpContext.Current.Session; } }

    public BindingList<InvoiceItem> GetItems() {
        BindingList<InvoiceItem> items = Session["InvoiceItems"] as BindingList<InvoiceItem>;
        if(items == null) {
            items = new BindingList<InvoiceItem>();
            Session["InvoiceItems"] = items;
        }
        return items;
    }
    public void Populate() {
        BindingList<InvoiceItem> res = GetItems();
        if(res.Count > 0) return;
        Random rnd = new Random();
        for(int n = 0; n < 10; n++) {
            InvoiceItem item = new InvoiceItem(res);
            item.Name = "Item" + res.Count.ToString();

            item.Price = ((decimal)rnd.Next(1, 1000)) / 10;
            item.Quantity = rnd.Next(1, 5);
            res.Add(item);
        }
    }
    public InvoiceItem GetItemById(int id) {
        foreach(InvoiceItem item in GetItems()) {
            if(item.Id == id) return item;
        }
        return null;
    }
    public void Delete(int id) {
        InvoiceItem item = GetItemById(id);
        if(item != null) {
            GetItems().Remove(item);
        }
    }
    public void Update(string name, int quantity, decimal price, int id) {
        Update(name, quantity, price, false, id);
    }
    public void Update(string name, int quantity, decimal price, bool isPaid, int id) {
        InvoiceItem item = GetItemById(id);
        if(item != null) {
            UpdateItem(item, name, quantity, price, isPaid);
        }
    }
    public void Insert(string name, int quantity, decimal price) {
        Insert(name, quantity, price, false);
    }
    public void Insert(string name, int quantity, decimal price, bool isPaid) {
        InvoiceItem item = new InvoiceItem(GetItems());
        UpdateItem(item, name, quantity, price, isPaid);
        GetItems().Add(item);
    }
    void UpdateItem(InvoiceItem item, string name, int quantity, decimal price, bool isPaid) {
        item.Name = name;
        item.Quantity = quantity;
        item.Price = price;
        item.IsPaid = isPaid;
    }
}

