﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.8944
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

'
'This source code was auto-generated by xsd, Version=2.0.50727.1432.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class thing
    
    Private propField() As thingProp
    
    Private thing1Field As thingThing
    
    Private thingsField() As thingThings
    
    Private typeField As String
    
    Private nameField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("prop")>  _
    Public Property prop() As thingProp()
        Get
            Return Me.propField
        End Get
        Set
            Me.propField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("thing")>  _
    Public Property thing1() As thingThing
        Get
            Return Me.thing1Field
        End Get
        Set
            Me.thing1Field = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("things")>  _
    Public Property things() As thingThings()
        Get
            Return Me.thingsField
        End Get
        Set
            Me.thingsField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true)>  _
Partial Public Class thingProp
    
    Private nameField As String
    
    Private typeField As String
    
    Private valueField As Byte
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property value() As Byte
        Get
            Return Me.valueField
        End Get
        Set
            Me.valueField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true)>  _
Partial Public Class thingThing
    
    Private refField As String
    
    Private typeField As String
    
    Private nameField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property ref() As String
        Get
            Return Me.refField
        End Get
        Set
            Me.refField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true)>  _
Partial Public Class thingThings
    
    Private thingField() As thingThingsThing
    
    Private nameField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("thing")>  _
    Public Property thing() As thingThingsThing()
        Get
            Return Me.thingField
        End Get
        Set
            Me.thingField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true)>  _
Partial Public Class thingThingsThing
    
    Private refField As String
    
    Private typeField As String
    
    Private nameField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property ref() As String
        Get
            Return Me.refField
        End Get
        Set
            Me.refField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
End Class