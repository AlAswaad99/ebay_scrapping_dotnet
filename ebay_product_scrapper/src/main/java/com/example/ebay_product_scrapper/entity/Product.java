package com.example.ebay_product_scrapper.entity;

import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Table(name = "Products")
public class Product {
    @Id
    @GeneratedValue
    @Column(name = "Id", nullable = false)
    private UUID id;

    @Column(name = "Title", nullable = false)
    private String title;
    @Column(name = "Price")
    private String price;
    @Column(name = "Description")
    private String description;
    @Column(name = "VideoUrl")
    private String videoUrl;
    @Column(name = "ItemNumber")
    private String itemNumber;

}
