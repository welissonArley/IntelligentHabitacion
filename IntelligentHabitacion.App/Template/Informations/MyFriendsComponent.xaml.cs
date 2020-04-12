using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Informations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFriendsComponent : ContentView
    {
        public string FriendName
        {
            get => (string)GetValue(FriendNameProperty);
            set => SetValue(FriendNameProperty, value);
        }
        public string EmergencyContact1Name
        {
            get => (string)GetValue(EmergencyContact1NameProperty);
            set => SetValue(EmergencyContact1NameProperty, value);
        }
        public string EmergencyContact2Name
        {
            get => (string)GetValue(EmergencyContact2NameProperty);
            set => SetValue(EmergencyContact2NameProperty, value);
        }
        public string EmergencyContact1Phonenumber
        {
            get => (string)GetValue(EmergencyContact1PhonenumberProperty);
            set => SetValue(EmergencyContact1PhonenumberProperty, value);
        }
        public string EmergencyContact2Phonenumber
        {
            get => (string)GetValue(EmergencyContact2PhonenumberProperty);
            set => SetValue(EmergencyContact2PhonenumberProperty, value);
        }
        public string EmergencyContact1RelationshipTo
        {
            get => (string)GetValue(EmergencyContact1RelationshipToProperty);
            set => SetValue(EmergencyContact1RelationshipToProperty, value);
        }
        public string EmergencyContact2RelationshipTo
        {
            get => (string)GetValue(EmergencyContact2RelationshipToProperty);
            set => SetValue(EmergencyContact2RelationshipToProperty, value);
        }
        public string FriendPhonenumber1
        {
            get => (string)GetValue(FriendPhonenumber1Property);
            set => SetValue(FriendPhonenumber1Property, value);
        }
        public string FriendPhonenumber2
        {
            get => (string)GetValue(FriendPhonenumber2Property);
            set => SetValue(FriendPhonenumber2Property, value);
        }

        public static readonly BindableProperty FriendNameProperty = BindableProperty.Create(
                                                        propertyName: "FriendName",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendNameChanged);

        public static readonly BindableProperty EmergencyContact1NameProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContact1Name",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContact1NameChanged);

        public static readonly BindableProperty EmergencyContact2NameProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContact2Name",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContact2NameChanged);

        public static readonly BindableProperty EmergencyContact1PhonenumberProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContact1Phonenumber",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContact1PhonenumberChanged);
        public static readonly BindableProperty EmergencyContact2PhonenumberProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContact2Phonenumber",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContact2PhonenumberChanged);
        public static readonly BindableProperty EmergencyContact1RelationshipToProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContact1RelationshipTo",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContact1RelationshipToChanged);
        public static readonly BindableProperty EmergencyContact2RelationshipToProperty = BindableProperty.Create(
                                                        propertyName: "EmergencyContact2RelationshipTo",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: EmergencyContact2RelationshipToChanged);
        public static readonly BindableProperty FriendPhonenumber1Property = BindableProperty.Create(
                                                        propertyName: "FriendPhonenumber1",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendPhonenumber1Changed);
        public static readonly BindableProperty FriendPhonenumber2Property = BindableProperty.Create(
                                                        propertyName: "FriendPhonenumber2",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(MyFriendsComponent),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: FriendPhonenumber2Changed);

        private static void FriendNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelFriendsName.Text = (string)newValue;
        }
        private static void EmergencyContact1NameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelEmergencyContact1Name.Text = (string)newValue;
        }
        private static void EmergencyContact2NameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelEmergencyContact2Name.Text = (string)newValue;
        }
        private static void EmergencyContact1PhonenumberChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelEmergencyContact1Phonenumber.Text = (string)newValue;
        }
        private static void EmergencyContact2PhonenumberChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelEmergencyContact2Phonenumber.Text = (string)newValue;
        }
        private static void EmergencyContact1RelationshipToChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelEmergencyContact1RelationshipTo.Text = (string)newValue;
        }
        private static void EmergencyContact2RelationshipToChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelEmergencyContact2RelationshipTo.Text = (string)newValue;
        }
        private static void FriendPhonenumber1Changed(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelFriendPhonenumber1.Text = (string)newValue;
        }
        private static void FriendPhonenumber2Changed(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyFriendsComponent)bindable).LabelFriendPhonenumber2.Text = (string)newValue;
            if(newValue != null)
                ((MyFriendsComponent)bindable).LabelSeparator.IsVisible = true;
        }

        public MyFriendsComponent()
        {
            InitializeComponent();
        }
    }
}