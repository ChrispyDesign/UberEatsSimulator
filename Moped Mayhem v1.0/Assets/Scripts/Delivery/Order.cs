﻿// Main Author - Christoper Bowles
//	Alterations by -
//
// Date last worked on 25/10/18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Color m_OrderColor;
	public DeliveryDropoff m_DropOffZone;
    public Food m_Food;
	public float m_fStartTime;		// When it starts
	public float m_fOrderExiryTime; // How Long it exists
	public _BDeliveryIndicator m_DeliveryIndicator;

	[HideInInspector]
	public OrderManager m_OrderManager;

	public void Success()
	{
		m_OrderManager.OrderSuccess(this);
	}
}
